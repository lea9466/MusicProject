using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
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
            item.Date = DateOnly.FromDateTime(DateTime.Today); 
            _context.Users.Add(item);
            _context.save();
            return item;
        }

        public void UpdateItem(int id, User item)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                throw new Exception("User not found");

            // שימוש ב-IsNullOrWhiteSpace מונע דריסה עם מחרוזת ריקה ""
            if (!string.IsNullOrWhiteSpace(item.Name))
                existing.Name = item.Name;

            if (!string.IsNullOrWhiteSpace(item.srcImage))
                existing.srcImage = item.srcImage;

            if (!string.IsNullOrWhiteSpace(item.Email))
                existing.Email = item.Email;

            if (!string.IsNullOrWhiteSpace(item.Password))
                existing.Password = item.Password;

            // עכשיו הבדיקה הזו תעבוד רק אם באמת שלחת Role חדש
            if (item.Role.HasValue)
                existing.Role = item.Role.Value;

            _context.save(); // וודאי שכתוב SaveChanges()
        }

        public void DeleteItem(int id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            item.IsActive= false;
            if (item != null)
            {
                _context.save();
            }
        }
    }
}