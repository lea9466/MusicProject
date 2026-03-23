using MusicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface IGemini
    {
        public Task<string> GetEnhancedChordsJsonAsync(FullSongDto request);
    }
}
