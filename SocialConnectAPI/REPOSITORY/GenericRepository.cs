using Microsoft.EntityFrameworkCore;
using MODEL;
using REPOSITORY.Repository;
using System.Linq.Expressions;

namespace REPOSITORY
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return await _entities.Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.CountAsync(predicate);
        }


        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
        }

        public async Task AddMultiple(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _entities.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public void DeleteMultiple(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _entities.RemoveRange(entities);
        }
        public IQueryable<T> GetByExpEagerly(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            try
            {
                var query = _context.Set<T>().AsNoTracking();

                // Dynamically include navigation properties
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }

                return query.Where(predicate);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                // Example: _logger.LogError(ex, "An error occurred while retrieving data.");

                throw; // Preserve stack trace
            }
        }
        public async Task<IEnumerable<T>> GetAll() => await _entities.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsyncWithPagination(int page, int pageSize)
        {
            if (page < 1 || pageSize <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0 and page size must be greater than 0.");
            }

            var totalCount = await _entities.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            return await _entities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsyncWithPaginationByDesc<TKey>(int page, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            if (page < 1 || pageSize <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0 and page size must be greater than 0.");
            }

            var totalCount = await _entities.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            return await _entities
                .OrderByDescending(orderBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T?> GetById(int id) => await _entities.FindAsync(id);
        public async Task<T?> GetByint(int id) => await _entities.FindAsync(id);

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            return entity;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
        }

        public void UpdateMultiple(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _entities.UpdateRange(entities);
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return await _entities.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionWithPagination(Expression<Func<T, bool>> expression, int page, int pageSize)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (page < 1 || pageSize <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0 and page size must be greater than 0.");
            }

            var totalCount = await _entities.CountAsync(expression);
            if (totalCount == 0)
            {
                return null;
            }
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            return await _entities.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionWithPaginationByDesc<TKey>(Expression<Func<T, bool>> expression, int page, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (page < 1 || pageSize <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0 and page size must be greater than 0.");
            }

            var totalCount = await _entities.CountAsync(expression);
            if (totalCount == 0)
            {
                return null;
            }
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            return await _entities.OrderByDescending(orderBy).Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<bool> Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await _entities.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Ideally use a logging framework instead of Console.WriteLine
                Console.WriteLine($"Error creating entity: {ex.Message}");
                return false;
            }
        }

        public List<T> GetByExp(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            try
            {
                return _entities.Where(predicate).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                // Ideally use a logging framework instead of Console.WriteLine
                Console.WriteLine($"Error retrieving entities by expression: {ex.Message}");
                throw;
            }
        }

        public IQueryable<T> GetByExpIQueryable(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            try
            {
                return _entities.Where(predicate).AsNoTracking();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving entities by expression: {ex.Message}");
                throw;
            }
        }

    }
}
