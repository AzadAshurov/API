using API.DTOs.Color;

namespace API.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<GetColorDTO>> GetAllAsync(int page, int take);
        Task<GetColorDetailDTO> GetByIdAsync(int id);
        Task<bool> CreateColorAsync(CreateColorDTO ColorDTO);
        Task UpdateColorAsync(int id, UpdateColorDTO ColorDTO);
        Task DeleteColorAsync(int id);
    }
}
