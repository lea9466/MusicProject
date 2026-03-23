using MusicDTO;
using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface IWordLine
    {
        public WordLineDto AddWordLine(WordLineDto wordLineDto);
        public List<WordLineDto> GetLinesBySongId(int songId);
        public void DeleteBySongId(int id);

    }
}
