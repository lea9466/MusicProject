using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser service;
        public UserController(IUser userService)
        {
            service = userService;
        }

        [HttpGet]
        public List<UserDto> Get()
        {
            return service.GetAllUsers();
        }

        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return null;
        }
        //[Authorize]
        //[HttpPut("update-profile")]
        //public IActionResult UpdateProfile([FromBody] UserDto dto)
        //{
        //    // 1. קבלת ה-ID של המשתמש הנוכחי מה-Token
        //    var userId = User.FindFirst();

        //    // 2. קריאה לשירות לעדכון הפרטים (שם, טלפון וכו')
        //    var success = _userService.UpdateGeneralInfo(userId, dto);

        //    if (!success) return BadRequest("עדכון הפרטים נכשל");

        //    return Ok("הפרופיל עודכן בהצלחה");
        //}


        [HttpPost]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            // 1. שמירת המשתמש ב-Database (דרך ה-Service שלך)
            var newUser = service.Register(userDto);

            if (newUser == null) return BadRequest("שגיאה ביצירת המשתמש");

            var token = service.GenerateJwtToken(newUser);

            // 3. החזרת אובייקט אנונימי שמכיל את שניהם
            return Ok(new
            {
                Token = token,
                User = newUser
            });

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto request)
        {
            UserDto u = service.Login(request.Password, request.Email);
            if (u == null) return BadRequest("משתמש לא נמצא");

            var token = service.GenerateJwtToken(u);

            // 3. החזרת אובייקט אנונימי שמכיל את שניהם
            return Ok(new
            {
                Token = token,
                User = u
            });
        }

        [Authorize]
        [HttpPost("setNameOrImg")]
        public IActionResult UpdateNameOrImg([FromBody] UserDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                service.UpdateUser(userId, request);
                return NoContent(); // 204 → הצלחה
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // 404 → משתמש לא נמצא
            }
        }

        [Authorize]
        [HttpPost("setEmailOrPass")]
        public IActionResult UpdateEmailOrPass([FromBody] UserDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var user = service.GetById(userId);
                if (request.Password == user.Password)
                {
                    request.Password = request.NewPass;
                    service.UpdateUser(userId, request);
                    return NoContent();
                } // 204 → הצלחה
                else return BadRequest("now password");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // 404 → משתמש לא נמצא
            }
        }
    }
}
