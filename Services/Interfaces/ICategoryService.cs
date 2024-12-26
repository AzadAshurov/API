namespace API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take);
        Task<GetCategoryDetailDTO> GetByIdAsync(int id);
        Task<bool> CreateCategoryAsync(CreateCategoryDTO categoryDTO);
        Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDTO);
        Task DeleteCategoryAsync(int id);
    }
}
