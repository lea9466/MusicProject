using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChordController : ControllerBase
    {
        private readonly IChord service;
        public ChordController(IChord chordService)
        {
            service = chordService;
        }
        [HttpGet]
        public List<ChordDto> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public ChordDto Get(int id)
        {
            return null;
        }

        [HttpPost]
        public void Post([FromBody] ChordDto ChordDto)
        {
            service.CreateChord(ChordDto);
           
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
