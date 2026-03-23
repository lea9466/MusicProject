using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Security.Claims;

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoriteSongController : ControllerBase
    {
        private readonly IUserFavoriteSong service;
        public UserFavoriteSongController(IUserFavoriteSong UFSService)
        {
            service = UFSService;
        }
       
        [HttpGet]
        public List<CategoryDto> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return null;
        }

        [Authorize]
        [HttpPost]
        public UserFavoriteSongDto Post([FromBody] UserFavoriteSongDto UDSD)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return service.ToggleFavorite(UDSD,userId);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
