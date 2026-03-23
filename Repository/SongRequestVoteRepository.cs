using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicIinterfaces;
using MusicInterfaces;
using MusicModels;

namespace Repository
{
    public class SongRequestVoteRepository : IRepository<SongRequestVote>, ICompositeDelete<SongRequestVote>
    {
        private readonly IContext _context;

        public SongRequestVoteRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<SongRequestVote> GetAll()
        {
            return _context.songRequestVotes
                .Include(v => v.User)
                .Include(v => v.SongRequest);
        }

        public SongRequestVote GetById(int id)
        {
            // מחזיר את ההצבעה הראשונה שקשורה לבקשה (לפי ה-Id שנשלח)
            return _context.songRequestVotes.FirstOrDefault(x => x.SongRequestId == id);
        }

        public SongRequestVote AddItem(SongRequestVote item)
        {
            // בדיקה למניעת כפילות לפני ההוספה
            var exists = _context.songRequestVotes
                .Any(v => v.UserId == item.UserId && v.SongRequestId == item.SongRequestId);

            if (!exists)
            {
                _context.songRequestVotes.Add(item);
                _context.save();
                return item;
            }
            return null;
        }

        public void UpdateItem(int id, SongRequestVote item)
        {
            // בדרך כלל בהצבעות רק מעדכנים את תאריך ההצבעה אם צריך
            var existing = _context.songRequestVotes
                .FirstOrDefault(x => x.SongRequestId == id && x.UserId == item.UserId);

            if (existing != null)
            {
                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            // מוחק את כל ההצבעות שקשורות לבקשה מסוימת (למשל אם הבקשה נמחקה)
            var items = _context.songRequestVotes.Where(x => x.SongRequestId == id);
            if (items.Any())
            {
                _context.songRequestVotes.RemoveRange(items);
                _context.save();
            }
        }

        // מימוש ה-Interface למחיקה לפי שני המפתחות (ביטול הצבעה של משתמש ספציפי)
        public void DeleteByKeys(int key1, int key2)
        {
            // key1 = UserId, key2 = SongRequestId
            var item = _context.songRequestVotes
                .FirstOrDefault(x => x.UserId == key1 && x.SongRequestId == key2);

            if (item != null)
            {
                _context.songRequestVotes.Remove(item);
                _context.save();
            }
        }
    }
}