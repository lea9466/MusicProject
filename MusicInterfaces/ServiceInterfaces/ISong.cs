using MusicDTO;
using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface ISong
    {
        public List<SongDto> GetAllSongs();
        public SongDto AddSong(SongDto songDto);
        public bool AddFullSong(FullSongDto fullSongDto);
        public FullSongDto GetFullSongById(int songId);
        public List<SongDto> GetSongsByIds(List<int> ids);
        public List<SongDto> GetSongsByUserId(int ID);
        public bool DeleteSong(int Id);
        public SongDto UpdateSong(FullSongDto fullSongDto);
        public List<SongDto> GetSongsByCatId(int id);
        public List<SongDto> GetNewSongs();
        public List<SongDto> Search(SearchObjDto searchObj);

    }
}
