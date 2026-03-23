using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicIinterfaces;
using MusicModels;
namespace Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly IContext _context;

        public CategoryRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Songs);
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Category AddItem(Category item)
        {
            _context.Categories.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, Category item)
        {
            var existing = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                existing.Name = item.Name ?? existing.Name;
                existing.Description = item.Description ?? existing.Description;
                _context.save();
            }
        }

        public void DeleteItem(int id)
        {
            var item = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                _context.save();
            }
        }
    }
}
