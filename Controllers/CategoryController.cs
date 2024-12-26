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

            var categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {
            if (!await _categoryService.CreateCategoryAsync(categoryDTO))
                return BadRequest();

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO categoryDTO)
        {
            if (id < 1)
                return BadRequest();

            await _categoryService.UpdateCategoryAsync(id, categoryDTO);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _categoryService.DeleteCategoryAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
