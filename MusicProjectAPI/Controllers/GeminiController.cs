using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Text.Json;

namespace MusicProjectAPI.Controllers
{
    public class GeminiSongResponse
    {
        public List<ChordDto> Chords { get; set; }
        public string MusicalRecommendations { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : Controller
    {
        private readonly IGemini service;
        public GeminiController(IGemini Gservice)
        {
            service = Gservice;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SuggestImprovements([FromBody] FullSongDto request)
        {
            // 1. קריאה לשירות (אם השירות יפיל שגיאה, ה-Middleware יתפוס אותה)
            string rawResponse = await service.GetEnhancedChordsJsonAsync(request);

            // 2. ניקוי ה-JSON (לוגיקה חיונית)
            int start = rawResponse.IndexOf('{');
            int end = rawResponse.LastIndexOf('}');

            if (start == -1 || end == -1)
            {
                // מחזירים אובייקט אחיד שה-Interceptor בריאקט ידע לקרוא
                return BadRequest(new { message = "הבינה המלאכותית לא החזירה פורמט JSON תקין." });
            }

            string jsonOnly = rawResponse.Substring(start, end - start + 1);

            // 3. דסריאליזציה
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<GeminiSongResponse>(jsonOnly, options);

            // 4. החזרת תוצאה
            return Ok(result);
        }

    }
}
