using API.DTOs.Product;
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

        public async Task<GetCategoryDetailDTO> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, nameof(Category.Products));

            if (category == null) return null;

            GetCategoryDetailDTO categoryDTO = new()
            {
                Id = category.Id,
                Name = category.Name,
                ProductsDTOs = category.Products?.Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList() ?? new List<GetProductDTO>()
            };

            return categoryDTO;
        }


    }
}
