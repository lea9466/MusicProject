using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDTO
{
    public class FullSongDto
    {
        public SongDto Song { get; set; }
        public List<WordLineDto> WordLines { get; set; }
        public List<ChordDto> Chords { get; set; }
        public Dictionary<int, List<ChordDto>>? ChordsByLine { get; set; }
    }
}
