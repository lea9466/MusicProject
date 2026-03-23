using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicModels
{
    public class WordLine
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int LineNumber { get; set; }    
        public string Text { get; set; }       
    }
}
