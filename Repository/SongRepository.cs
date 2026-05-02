using Microsoft.EntityFrameworkCore;
using MusicIinterfaces;
using MusicModels;

namespace Repository
{
    public class SongRepository : IRepository<Song>
    {
        private readonly IContext _context;

        public SongRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<Song> GetAll()
        {
            // כאן הוספתי Include כדי שכשתשלוף שיר, תקבל גם את המידע על הקטגוריה והסולם שלו
            return _context.Songs
                .Include(s => s.Category);
                
        }

        public Song GetById(int id)
        {
            return _context.Songs
                .Include(s => s.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public Song AddItem(Song item)
        {
            _context.Songs.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, Song item)
        {
            var existing = _context.Songs.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                // עדכון כל שדות השיר
                existing.Name = item.Name ?? existing.Name;
                existing.CategoryId = item.CategoryId;
                //existing.ScaleId = item.ScaleId;
                //existing.Chords = item.Chords;
                existing.Artist = item.Artist ?? existing.Artist;
                existing.UtubLink = item.UtubLink ?? existing.UtubLink;
                //existing.Degree = item.Degree;
                //existing.Date = item.Date;
                existing.ViewsCount = item.ViewsCount ;
                existing.Language = item.Language ?? existing.Language;
                existing.scale = item.scale ?? existing.scale;
                if (existing.SourceText != item.SourceText)
                    existing.SourceText = item.SourceText;
                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            var item = _context.Songs.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Songs.Remove(item);
                _context.save();
            }
        }
    }
}