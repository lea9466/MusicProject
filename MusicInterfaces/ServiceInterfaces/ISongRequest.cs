using MusicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface ISongRequest
    {
        public SongRequestDto AddSongRequest(SongRequestDto songRequestDto);
        public List<SongRequestDto> GetAllSongRequests(int userId);
        public void FullReq(SongRequestDto srd);

    }
}
