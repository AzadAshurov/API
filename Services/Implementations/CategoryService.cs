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
        public async Task<bool> CreateCategoryAsync(CreateCategoryDTO categoryDTO)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name))
                return false;

            await _categoryRepository.AddAsync(new Category
            {
                Name = categoryDTO.Name
            });

            await _categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Not found");

            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name && c.Id == id))
                throw new Exception("Exists");

            category.Name = categoryDTO.Name;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Not found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }

    }
}
