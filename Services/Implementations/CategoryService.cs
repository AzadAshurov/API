

using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<GetCategoryDTO> categories = await _categoryRepository.GetAll(skip: (page - 1) * take, take: take)
                .Select(x => new GetCategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductCount = x.Products.Count

                }).ToListAsync();
            return categories;

        }

    }
}
