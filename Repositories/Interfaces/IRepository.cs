namespace API.Repositories.Interfaces
{
    public interface IRepository
    {
        IQueryable<Category> GetAll();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<int> SaveChangesAsync();
    }
}
