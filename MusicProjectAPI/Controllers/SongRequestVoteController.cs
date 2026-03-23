using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;
using System.Security.Claims;

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongRequestVoteController : ControllerBase
    {
        private readonly ISongRequestVot service;
        public SongRequestVoteController(ISongRequestVot SRVervice)
        {
            service = SRVervice;
        }

        [Authorize]
        [HttpPost("{songId}")]
        public IActionResult Post(int songId)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
            {
                return Unauthorized("לא נמצא מזהה משתמש בטוקן");
            }
            var userId = int.Parse(nameIdentifier.Value);
            var result = service.ToggleVote(userId, songId);
            if (result == null) return NotFound("השיר לא נמצא");
            string message = result.IsVoted ? "ההצבעה נוספה בהצלחה!" : "ההצבעה בוטלה בהצלחה";

            return Ok(new
            {
                Message = message,
                Status = result.IsVoted, // "true" להוספה, "false" לביטול
                //NewScore = result.NewPriorityScore
            });
        }
    }
}
