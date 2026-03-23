using MusicDTO;
using MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces.ServiceInterfaces
{
    public interface IUser
    {
        public UserDto Register(UserDto userDto);

        public UserDto Login(string email, string password);
        public List<UserDto> GetAllUsers();

        public string GenerateJwtToken(UserDto user);
        public void UpdateNameOrImg(int id, UserDto user);


    }
}
