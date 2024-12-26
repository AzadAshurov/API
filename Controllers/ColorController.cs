using API.DTOs.Color;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IRepository<Color> _repository;
        private readonly IColorService _ColorService;

        public ColorController(IRepository<Color> repository, IColorService ColorService)
        {
            _repository = repository;
            _ColorService = ColorService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return StatusCode(StatusCodes.Status200OK, await _ColorService.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var ColorDTO = await _ColorService.GetByIdAsync(id);

            if (ColorDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, ColorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateColorDTO ColorDTO)
        {
            if (!await _ColorService.CreateColorAsync(ColorDTO))
                return BadRequest();

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateColorDTO ColorDTO)
        {
            if (id < 1)
                return BadRequest();

            await _ColorService.UpdateColorAsync(id, ColorDTO);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _ColorService.DeleteColorAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
