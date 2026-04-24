using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using MusicIinterfaces;
using MusicInterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
using Repository;
using Service.services;
using System.Text;

namespace MusicProjectAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // ✅ פורט דינמי ל-Railway
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddScoped<ICategory, CategoryService>();
                builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
                builder.Services.AddScoped<IUser, UserService>();
                builder.Services.AddScoped<IRepository<User>, UserRepository>();
                builder.Services.AddScoped<IChord, ChordService>();
                builder.Services.AddScoped<IRepository<Chord>, ChordRepository>();
                builder.Services.AddScoped<ISong, SongService>();
                builder.Services.AddScoped<IRepository<Song>, SongRepository>();
                builder.Services.AddScoped<IWordLine, WordLineService>();
                builder.Services.AddScoped<IRepository<WordLine>, WordLineRepository>();
                builder.Services.AddScoped<IUserFavoriteSong, UserFavoriteSongService>();
                builder.Services.AddScoped<IRepository<UserFavoriteSong>, UserFavoriteSongRepository>();
                builder.Services.AddHttpClient<IGemini, GeminiMusicService>();
                builder.Services.AddScoped<ICompositeDelete<UserFavoriteSong>, UserFavoriteSongRepository>();
                builder.Services.AddScoped<ISongRequest, SongRequestService>();
                builder.Services.AddScoped<IRepository<SongRequest>, SongRequestRepository>();
                builder.Services.AddScoped<ISongRequestVot, SongRequestVoteService>();
                builder.Services.AddScoped<IRepository<SongRequestVote>, SongRequestVoteRepository>();
                builder.Services.AddScoped<IContext, MusicDB.DataBase>();

                builder.Services.AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                }, typeof(Program).Assembly);

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowReactApp",
                        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });

                // ✅ JWT עם בדיקת null
                var jwtKey = builder.Configuration["Jwt:Key"]
                    ?? throw new Exception("❌ Jwt:Key is missing!");
                var jwtIssuer = builder.Configuration["Jwt:Issuer"]
                    ?? throw new Exception("❌ Jwt:Issuer is missing!");
                var jwtAudience = builder.Configuration["Jwt:Audience"]
                    ?? throw new Exception("❌ Jwt:Audience is missing!");

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

                var app = builder.Build();

                app.UseCors("AllowReactApp");

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;

                        var errorResponse = new
                        {
                            message = "שגיאת שרת פנימית",
                            details = exception?.Message
                        };

                        await context.Response.WriteAsJsonAsync(errorResponse);
                    });
                });

                app.UseSwagger();
                app.UseSwaggerUI();

                // ❌ הוסר UseHttpsRedirection
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== FATAL ERROR ===");
                Console.WriteLine($"Type: {ex.GetType().FullName}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Inner: {ex.InnerException?.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                Console.WriteLine("===================");
                throw;
            }
        }
    }
}
