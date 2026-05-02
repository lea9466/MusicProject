
using MusicModels;
namespace MusicDTO
{
   
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? srcImage { get; set; }
        public string? NewPass { get; set; }
        public UserRole Role { get; set; }
        public DateOnly? Date { get; set; } 

        public List<int>? FavoriteSongs { get; set; }

    }
}
