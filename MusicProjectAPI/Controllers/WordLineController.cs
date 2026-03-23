using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using MusicModels;

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordLineController : ControllerBase
    {
        private readonly IWordLine service;
        public WordLineController(IWordLine WLService)
        {
            service = WLService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public List<CategoryDto> Get()
        {
            return null;
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return null;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public WordLineDto Post([FromBody] WordLineDto wordLineDto)
        {
           return service.AddWordLine(wordLineDto);
        }

        // PUT api/<CategoryController>/5
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
