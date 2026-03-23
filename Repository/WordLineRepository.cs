using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicIinterfaces;
using MusicModels;

namespace Repository
{
    public class WordLineRepository : IRepository<WordLine>
    {
        private readonly IContext _context;

        public WordLineRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<WordLine> GetAll()
        {
            return _context.WordLines;
        }

        public WordLine GetById(int id)
        {
            return _context.WordLines.FirstOrDefault(x => x.Id == id);
        }

        public WordLine AddItem(WordLine item)
        {
            _context.WordLines.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, WordLine item)
        {
            var existing = _context.WordLines.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                existing.SongId = item.SongId;
                existing.LineNumber = item.LineNumber;
                existing.Text = item.Text;

                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            var item = _context.WordLines.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.WordLines.Remove(item);
                _context.save();
            }
        }
    }
}