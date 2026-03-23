using MusicDTO;
using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface IUserFavoriteSong
    {
        public List<int> GetFavoiteSongsByUserId(int userId);
        public UserFavoriteSongDto ToggleFavorite(UserFavoriteSongDto UFS, int userId);
        public void DeleteBySongId(int id);

    }
}
