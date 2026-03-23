using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicIinterfaces;
using MusicModels;

namespace Repository
{
    public class ChordRepository : IRepository<Chord>
    {
        private readonly IContext _context;

        public ChordRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<Chord> GetAll()
        {
            return _context.Chords;
        }

        public Chord GetById(int id)
        {
            return _context.Chords.FirstOrDefault(x => x.Id == id);
        }

        public Chord AddItem(Chord item)
        {
            _context.Chords.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, Chord item)
        {
            var existing = _context.Chords.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                // עדכון כל מאפייני האקורד
                existing.Name = item.Name;
                existing.Degree = item.Degree;
                existing.LineNumber = item.LineNumber;
                existing.IndexInLine = item.IndexInLine;
                existing.SongId = item.SongId;
                existing.Spaces = item.Spaces;
                existing.Adding = item.Adding;

                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            var item = _context.Chords.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Chords.Remove(item);
                _context.save();
            }
        }
    }
}