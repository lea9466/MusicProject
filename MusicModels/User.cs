using System.ComponentModel.DataAnnotations;

namespace MusicModels
{

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? srcImage { get; set; }
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public virtual ICollection<UserFavoriteSong> FavoriteSongs { get; set; } = new List<UserFavoriteSong>();
        public virtual ICollection<SongRequest> SongRequests { get; set; } = new List<SongRequest>();
        public virtual ICollection<SongRequest> FulfilledRequests { get; set; } = new List<SongRequest>();
        public virtual ICollection<SongRequestVote> SongVotes { get; set; } = new List<SongRequestVote>();
        public virtual ICollection<Song> MySongs { get; set; } = new List<Song>();
        public UserRole? Role { get; set; } = UserRole.Regular;
        public bool IsActive { get; set; } = true;

    }
}
