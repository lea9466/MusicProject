using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Security.Claims;



namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongRequestController : ControllerBase
    {
        private readonly ISongRequest service;
        public SongRequestController(ISongRequest songRequestService)
        {
            service = songRequestService;
        }

        [Authorize]
        [HttpPost]
        public SongRequestDto Post([FromBody] SongRequestDto songRequestDto)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
            {
                return null;
            }
            var userId = int.Parse(nameIdentifier.Value);
            songRequestDto.CreatorId = userId;
            return service.AddSongRequest(songRequestDto);
        }

        [Authorize]
        [HttpPut]
        public void Put([FromBody] SongRequestDto songRequestDto)
        {
            service.FullReq(songRequestDto);
        }


        [HttpGet]
        [AllowAnonymous]
        public List<SongRequestDto> Get()
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            {
                string authHeader = authHeaderValues.ToString();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring(7);

                    // בדיקה שהטוקן לא ריק לפני הניסיון לקרוא אותו
                    if (!string.IsNullOrWhiteSpace(token))
                    {

                        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
                        if (nameIdentifier == null)
                        {
                            return service.GetAllSongRequests(0);
                        }
                        var userId = int.Parse(nameIdentifier.Value);
                        return service.GetAllSongRequests(userId);
                    }
                }
            }
            return service.GetAllSongRequests(0);
        }
    }
}
