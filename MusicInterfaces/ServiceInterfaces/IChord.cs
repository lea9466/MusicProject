using MusicDTO;
using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface IChord
    {
        public ChordDto CreateChord(ChordDto chord);
        public ChordDto GetChordById(int id);
        Dictionary<int, List<ChordDto>> GetChordsBySongId(int songId);
        public void DeleteBySongId(int id);

    }
}
