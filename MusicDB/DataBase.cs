using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicIinterfaces;
using MusicModels;
namespace MusicDB
{
    public class DataBase : DbContext, IContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Chord> Chords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavoriteSong> UserFavoriteSongs { get; set; }
        public DbSet<WordLine> WordLines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SongRequest> SongRequests { get ; set; }
        public DbSet<SongRequestVote> songRequestVotes { get ; set ; }

        protected readonly IConfiguration Configuration;
        public DataBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DataBase()
        {
        }
        public void save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. מפתח משולב למועדפים
            modelBuilder.Entity<UserFavoriteSong>()
                .HasKey(x => new { x.UserId, x.SongId });

            modelBuilder.Entity<SongRequestVote>()
                .HasKey(x => new { x.UserId, x.SongRequestId });

            // הגדרת הקשרים עבור בקשת שיר (SongRequest)
            modelBuilder.Entity<SongRequest>(entity =>
            {
                // 1. קשר למשתמש שביקש (Creator)
                entity.HasOne(sr => sr.User)
                      .WithMany(u => u.SongRequests) // הרשימה של "הבקשות שלי" ב-User
                      .HasForeignKey(sr => sr.CreatorId)
                      .OnDelete(DeleteBehavior.NoAction); // מונע מחיקה בשרשרת

                // 2. קשר למשתמש שמילא את הבקשה (Fulfiller)
                entity.HasOne(sr => sr.Fulfiller)
                      .WithMany(u => u.FulfilledRequests) // הרשימה של "בקשות שמילאתי" ב-User
                      .HasForeignKey(sr => sr.FulfillerId)
                      .OnDelete(DeleteBehavior.NoAction); // מונע מחיקה בשרשרת
            });

            // הגדרת הקשר עבור הצבעות (Votes)
            modelBuilder.Entity<SongRequestVote>(entity =>
            {
                // הגדרת המפתח הכפול (מנעת כבר קודם, אבל נוודא את הקשרים)
                entity.HasKey(v => new { v.UserId, v.SongRequestId });

                entity.HasOne(v => v.User)
                      .WithMany(u => u.SongVotes)
                      .HasForeignKey(v => v.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(v => v.SongRequest)
                      .WithMany(r => r.Votes)
                      .HasForeignKey(v => v.SongRequestId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // 2. ברירת מחדל: מניעת מחיקה בשרשרת לכולם (מגן על הקטגוריות והמשתמשים שלך)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            modelBuilder.Entity<Song>()
                   .Property(s => s.Date)
                        .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // אם הגענו לכאן ו-Configuration ריק, סימן שאנחנו בזמן Migration
                // אפשר להשאיר את זה ככה, או לשים כאן את מחרוזת החיבור המקומית כברירת מחדל
                if (Configuration != null)
                {
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
            }
            // החלפנו את השרת המקומי בשרת המרוחק עם שם המשתמש והסיסמה החדשים
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-TQTK0I5;database=MusicDataBase;trusted_connection=true;TrustServerCertificate=true");
        //}
    }
}
