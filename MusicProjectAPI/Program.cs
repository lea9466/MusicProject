using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
                cfg.AddProfile<MappingProfile>(); // שם מחלקת הפרופיל שלך
            }, typeof(Program).Assembly);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

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
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            var app = builder.Build();
            app.UseCors("AllowReactApp");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
