using MusicDTO;
using MusicIinterfaces;
using MusicModels;

namespace Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext _context;

        public UserRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User AddItem(User item)
        {
            _context.Users.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, User item)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                throw new Exception("User not found");

            existing.Name = item.Name ?? existing.Name;
            existing.srcImage = item.srcImage ?? existing.srcImage;
            existing.Email = item.Email ?? existing.Email;
            existing.Password = item.Password ?? existing.Password;
            _context.save();
        }

        public void DeleteItem(int id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Users.Remove(item);
                _context.save();
            }
        }
    }
}