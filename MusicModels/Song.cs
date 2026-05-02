using System.ComponentModel.DataAnnotations;

namespace MusicModels
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? Artist { get; set; }
        public string? UtubLink { get; set; }
        public string SourceText { get; set; }
        public int? Degree { get; set; }
        public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public string? Language { get; set; }
        public string? scale { get; set; }
        public string? Tips {  get; set; }
        public int ViewsCount { get; set; } = 0;
        public int ChordLikesCount { get; set; } = 0;
        public virtual ICollection<Chord>? Chords { get; set; }
        public virtual ICollection<UserFavoriteSong>? FavoritedByUsers { get; set; }
        public virtual ICollection<WordLine>? WordLines { get; set; }
    }

}
