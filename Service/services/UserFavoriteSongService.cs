using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
using Repository;

namespace Service.services
{
    public class UserFavoriteSongService : IUserFavoriteSong
    {
        private readonly IRepository<UserFavoriteSong> _repository;
        private readonly ICompositeDelete<UserFavoriteSong> _repoDelete;

        public UserFavoriteSongService(
            IRepository<UserFavoriteSong> repoAdd,
            ICompositeDelete<UserFavoriteSong> repoDelete)
        {
            _repository = repoAdd;
            _repoDelete = repoDelete;
        }

        public UserFavoriteSongDto ToggleFavorite(UserFavoriteSongDto UFS,int userId)
        {
            // 1. חיפוש האם הקשר כבר קיים בבסיס הנתונים
            // (מניחים ש-repository.GetAll() מחזיר IQueryable או רשימה)
            var existing = _repository.GetAll()
                .FirstOrDefault(f => UFS.SongId==f.SongId && userId==f.UserId);

            if (existing != null)
            {
                // 2. אם קיים - מוחקים (Unlike)
                // כאן אנחנו משתמשים בפונקציה המיוחדת מהממשק החדש שהזרקת
                _repoDelete.DeleteByKeys(userId, UFS.SongId);

                // מחזירים אובייקט ריק או מסמנים בצורה כלשהי שהשיר הוסר
                return null;
            }
            else
            {
                // 3. אם לא קיים - מוסיפים (Like)
                var newFSong = new UserFavoriteSong
                {
                    SongId = UFS.SongId,
                    UserId = userId,
                };
                _repository.AddItem(newFSong);

                return new UserFavoriteSongDto
                {
                    SongId = newFSong.SongId
                };
            }
        }
        public void DeleteItem(UserFavoriteSongDto UFS)
        {
           
        }

        public List<int> GetFavoiteSongsByUserId(int userId)
        {
            return _repository.GetAll()
                     .Where(s => s.UserId == userId)
                     .Select(s => s.SongId)
                     .ToList();
        }
        public void DeleteBySongId(int id)
        {
            var itemsToDelete = _repository.GetAll()
                                           .Where(item => item.SongId == id)
                                           .ToList();
            foreach (var item in itemsToDelete)
            {
                _repository.DeleteItem(item.SongId);
            }
        }
        public UserFavoriteSongDto AddSong(UserFavoriteSongDto UFS)
        {
            throw new NotImplementedException();
        }
    }
}
