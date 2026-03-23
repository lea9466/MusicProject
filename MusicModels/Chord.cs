using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicModels
{

    public class Chord
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int? LineNumber { get; set; }
        public int? IndexInLine { get; set; }
        public int? SongId { get; set; }
        public Song Song { get; set; }
        public int? Spaces { get; set; }
        public string? Adding { get; set; }

    }

}
