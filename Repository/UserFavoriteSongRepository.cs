using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces;
using MusicModels;

namespace Repository
{
    public class UserFavoriteSongRepository : IRepository<UserFavoriteSong>, ICompositeDelete<UserFavoriteSong>
    {
        private readonly IContext _context;

        public UserFavoriteSongRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<UserFavoriteSong> GetAll()
        {
            return _context.UserFavoriteSongs
                .Include(uf => uf.User)
                .Include(uf => uf.Song);
        }

        // בטבלה מקשרת אין בדרך כלל Id בודד, אבל נממש לפי ה-Interface
        public UserFavoriteSong GetById(int id)
        {
            // כאן זה מחזיר את המועדף הראשון שקשור ל-Id שנשלח (למשל UserId)
            return _context.UserFavoriteSongs.FirstOrDefault(x => x.UserId == id);
        }

        public UserFavoriteSong AddItem(UserFavoriteSong item)
        {
            _context.UserFavoriteSongs.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, UserFavoriteSong item)
        {
            // בטבלה כזו בדרך כלל לא "מעדכנים" אלא מוחקים ומוסיפים מחדש,
            // אבל אם תרצה לעדכן שדות נוספים (אם יהיו בעתיד):
            var existing = _context.UserFavoriteSongs.FirstOrDefault(x => x.UserId == id && x.SongId == item.SongId);
            if (existing != null)
            {
                existing.UserId = item.UserId;
                existing.SongId = item.SongId;
                _context.save();
            }
        }

        

        public void DeleteItem(int id)
        {
            var item = _context.UserFavoriteSongs.FirstOrDefault(x => x.SongId == id);
            if (item != null)
            {
                _context.UserFavoriteSongs.Remove(item);
                _context.save();
            }
        }
       

        public void DeleteByKeys(int key1, int key2)
        {
            var item = _context.songRequestVotes.FirstOrDefault(x => x.UserId == key1 && x.SongRequestId == key2);
            if (item != null)
            {
                _context.songRequestVotes.Remove(item);
                _context.save();
            }
        }
    }
}