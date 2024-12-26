namespace API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take);
        Task<GetCategoryDetailDTO> GetByIdAsync(int id);

    }
}
