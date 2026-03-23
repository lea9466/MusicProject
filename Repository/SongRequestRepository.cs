using Microsoft.EntityFrameworkCore;
using MusicIinterfaces;
using MusicModels;
using System.Linq;

namespace Repository
{
    public class SongRequestRepository : IRepository<SongRequest>
    {
        private readonly IContext _context;

        public SongRequestRepository(IContext context)
        {
            _context = context;
        }
        public IQueryable<SongRequest> GetAll()
        {
            // אנחנו מוסיפים Include כדי לקבל את פרטי המשתמש ואת ההצבעות שלו
            return _context.SongRequests
                .Include(r => r.User)
                .Include(r => r.Votes)
                .Include(r => r.Fulfiller);
        }
        public SongRequest GetById(int id)
        {
            return _context.SongRequests
                .Include(r => r.User)
                .Include(r => r.Votes)
                .FirstOrDefault(x => x.Id == id);
        }

        public SongRequest AddItem(SongRequest item)
        {
            _context.SongRequests.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, SongRequest item)
        {
            var existing = _context.SongRequests.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                // עדכון השדות הרלוונטיים
                existing.SongDes = item.SongDes ?? existing.SongDes;
                existing.PriorityScore = item.PriorityScore ?? existing.PriorityScore;
                existing.IsFulfilled = item.IsFulfilled;
                existing.FulfillerId = item.FulfillerId ?? existing.FulfillerId;

                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            var item = _context.SongRequests.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.SongRequests.Remove(item);
                _context.save();
            }
        }
    }
}