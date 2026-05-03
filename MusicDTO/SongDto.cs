using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Artist { get; set; }
        public string? UtubLink { get; set; }
        public int? Degree { get; set; }
        public DateOnly? Date { get; set; }
        public string? Language { get; set; }
        public string? MajorOrMinor { get; set; }
        public string SourceText { get; set; }
        public string? Tips { get; set; }

        // מזהים בלבד - בלי האובייקטים המלאים
        public int? UserId { get; set; }
        public int CategoryId { get; set; }
        public int? ScaleId { get; set; }
        public string? CreatorName { get; set; }
        public int ViewsCount { get; set; } 
        public int ChordLikesCount { get; set; }
        public string? Credit { get; set; }


    }
}
