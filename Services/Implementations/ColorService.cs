using API.DTOs.Color;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class ColorService : IColorService

    {
        private readonly IColorRepository _ColorRepository;

        public ColorService(IColorRepository ColorRepository)
        {
            _ColorRepository = ColorRepository;
        }

        public async Task<IEnumerable<GetColorDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<GetColorDTO> categories = await _ColorRepository.GetAll(skip: (page - 1) * take, take: take)
                .Select(x => new GetColorDTO
                {
                    Id = x.Id,
                    Name = x.Name

                }).ToListAsync();
            return categories;

        }

        public async Task<GetColorDetailDTO> GetByIdAsync(int id)
        {
            Color Color = await _ColorRepository.GetByIdAsync(id);

            if (Color == null) return null;

            GetColorDetailDTO ColorDTO = new()
            {
                Id = Color.Id,
                Name = Color.Name
            };

            return ColorDTO;
        }
        public async Task<bool> CreateColorAsync(CreateColorDTO ColorDTO)
        {
            if (await _ColorRepository.AnyAsync(c => c.Name == ColorDTO.Name))
                return false;

            await _ColorRepository.AddAsync(new Color
            {
                Name = ColorDTO.Name
            });

            await _ColorRepository.SaveChangesAsync();

            return true;
        }

        public async Task UpdateColorAsync(int id, UpdateColorDTO ColorDTO)
        {
            Color Color = await _ColorRepository.GetByIdAsync(id);
            if (Color == null)
                throw new Exception("Not found");

            if (await _ColorRepository.AnyAsync(c => c.Name == ColorDTO.Name && c.Id == id))
                throw new Exception("Exists");

            Color.Name = ColorDTO.Name;
            _ColorRepository.Update(Color);
            await _ColorRepository.SaveChangesAsync();
        }

        public async Task DeleteColorAsync(int id)
        {
            Color Color = await _ColorRepository.GetByIdAsync(id);

            if (Color == null)
                throw new Exception("Not found");

            _ColorRepository.Delete(Color);
            await _ColorRepository.SaveChangesAsync();
        }

    }
}
