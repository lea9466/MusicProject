using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class SearchObjDto
    {
        public string? NameSong { get; set; }
        public string? NameArtist { get; set; }
        public List<ChordDto>? Chords { get; set; }
        public string? WordLine { get; set; }




    }
}
