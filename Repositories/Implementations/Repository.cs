using System.Linq.Expressions;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            Console.Beep();
            _context = context;
            _table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAll(
       Expression<Func<T, bool>>? expression = null,
       Expression<Func<T, object>>? orderExpression = null,
       int skip = 0,
       int take = 0,
       bool isDescending = false,
       bool isTracking = false,
       params string[]? includes)
        {
            IQueryable<T> query = _table;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = _getIncludes(query, includes);

            query = orderExpression != null ? (query = isDescending ? query.OrderByDescending(orderExpression) : query.OrderBy(orderExpression)) : query;
            query = query.Skip(skip);
            query = skip != 0 ? query.Take(take) : query;
            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (includes != null)
                query = _getIncludes(query, includes);
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        private IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
        {
            for (int i = 0; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> anyExpression)
        {
            return _table.AnyAsync(anyExpression);
        }

    }
}
