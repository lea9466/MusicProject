using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class WordLineDto
    {
        public int Id { get; set; }
        public int SongId { get; set; } 
        public int LineNumber { get; set; }
        public string Text { get; set; }
    }
}
