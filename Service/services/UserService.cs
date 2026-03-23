using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicDTO;
using MusicIinterfaces;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.services
{
    public class UserService : IUser
    {
        private readonly IRepository<User> _repository;
        private readonly IUserFavoriteSong _userFavoriteSong;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repository,
                           IUserFavoriteSong userFavoriteSong,
                           IConfiguration configuration,
                           IMapper mapper)
        {
            _repository = repository;
            _userFavoriteSong = userFavoriteSong;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserDto Login(string password, string email)
        {
            var user = _repository.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null) return null;
            var userDto = _mapper.Map<UserDto>(user);
            userDto.FavoriteSongs = _userFavoriteSong.GetFavoiteSongsByUserId(user.Id);
            return userDto;
        }

        public UserDto Register(UserDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);
            _repository.AddItem(newUser);

            return _mapper.Map<UserDto>(newUser);
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _repository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }

        public void UpdateNameOrImg(int id, UserDto dto)
        {
            var userUpdate = _mapper.Map<User>(dto);
            _repository.UpdateItem(id, userUpdate);
        }

        public string GenerateJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim("Role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}