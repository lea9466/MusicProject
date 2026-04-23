using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory service;
        public CategoryController(ICategory categoryService)
        {
            service = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public List<CategoryDto> Get()
        {
            return service.GetAllCategories();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return service.GetCategoryById(id);
        }

        [Authorize]
        [HttpPost]
        public CategoryDto Post([FromBody] CategoryDto categoryDto)
        {
            return service.CreateCategory(categoryDto);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            bool success = service.DeleteCategory(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPost("updateCat")]
        public void UpdateCat([FromBody] CategoryDto categoryDto)
        {
            int catId = categoryDto.Id ?? 0;
            service.UpdateCategory(catId, categoryDto);

        }
    }
}
