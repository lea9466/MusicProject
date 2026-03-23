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

        [HttpPost]
        public async Task<IActionResult> SuggestImprovements([FromBody] FullSongDto request)
        {
            try
            {
                string rawResponse = await service.GetEnhancedChordsJsonAsync(request);

                // מחפש את תחילת האובייקט וסופו (סוגריים מסולסלים במקום מרובעים)
                int start = rawResponse.IndexOf('{');
                int end = rawResponse.LastIndexOf('}');

                if (start == -1 || end == -1)
                    return BadRequest("הבינה המלאכותית לא החזירה פורמט JSON תקין.");

                string jsonOnly = rawResponse.Substring(start, end - start + 1);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // כאן אנחנו משתמשים במחלקה החדשה שיצרנו
                var result = JsonSerializer.Deserialize<GeminiSongResponse>(jsonOnly, options);

                // מחזירים את האובייקט המלא (כולל האקורדים וההמלצות)
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה בעיבוד הנתונים: {ex.Message}");
            }
        }


    }
}
