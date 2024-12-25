using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
        private readonly ICategoryService _categoryService;

        public CategoryController(IRepository<Category> repository, ICategoryService categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return StatusCode(StatusCodes.Status200OK, await _categoryService.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO category)
        {
            await _repository.AddAsync(new Category { Name = category.Name });
            await _repository.SaveChangesAsync();

            // return Created();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            _repository.Delete(category);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            category.Name = name;
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
