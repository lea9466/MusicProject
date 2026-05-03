using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Security.Claims;

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISong service;
        public SongController(ISong songService)
        {
            service = songService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public List<SongDto> Get()
        {
            return service.GetAllSongs();
        }

        [HttpGet("GetNewSongs")]
        public List<SongDto> GetNewSongs()
        {
            return service.GetNewSongs();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        

        // יופעל בכתובת: api/songs/full/5
        [HttpGet("full/{id}")]
        public async Task<FullSongDto> GetFull(int id)
        {
            // מחזיר את השיר עם כל המילים והאקורדים
            return await service.GetFullSongById(id);
        }

        [HttpPost("GetByIds")]
        public List<SongDto> Get([FromBody] List<int> ids)
        {
            return service.GetSongsByIds(ids);
        }
        [HttpGet("GetFlatSongs")]
        public List<SongDto> GetFlatSongs()
        {
            return service.GetFlatSongs();
        }

        [HttpGet("GetByCatId/{id}")] // שיניתי ל-HttpGet והוספתי לוכסן להפרדה
        public List<SongDto> Get(int id) // הוספת הפרמטר id כאן היא קריטית
        {
            return service.GetSongsByCatId(id);
        }

        [Authorize]
        [HttpPost("GetByUserId")]
        public List<SongDto> GetByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return service.GetSongsByUserId(userId);
        }

        // POST api/<CategoryController>
        //[HttpPost]
        //public SongDto Post([FromBody] SongDto songDto)
        //{
        //    return service.AddSong(songDto);
        //}

        [HttpPost]
        public bool Post([FromBody] FullSongDto fullSongDto)
        {
            return service.AddFullSong(fullSongDto);
        }

        [HttpPost("Search")]
        public List<SongDto> Post([FromBody] SearchObjDto searchObjDto)
        {
            return service.Search(searchObjDto);
        }
        [Authorize]
        [HttpPut]
        public SongDto Put( [FromBody] FullSongDto fullSongDto)
        {
           return service.UpdateSong(fullSongDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
           return service.DeleteSong(id);
        }

        [HttpPost("chord-likes/{songId}")]
        public async Task<IActionResult> ToggleChordLike(int songId, [FromQuery] bool isLike)
        {
            // המתנה לסיום הפעולה במסד הנתונים
            await service.ToggleChordLike(songId, isLike);
            return Ok(new { message = "Chord like updated successfully" });
        }
    }
}
