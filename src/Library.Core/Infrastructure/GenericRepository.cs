using Library.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Library.Core.Infrastructure
{
    public abstract class GenericRepository<CTX, T> : IGenericRepository<T> 
        where CTX : DbContext
        where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(CTX context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<int> CountAsync(Expression<System.Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().CountAsync(expression);
        }

        public async Task<T> FirstAsync(Expression<System.Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<System.Collections.Generic.List<T>> GetDataAsync(
            Expression<System.Func<T, bool>> expression = null,
            Func<System.Linq.IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null,
            int? take = null)
        {
            var query = _dbSet.AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(
            Expression<System.Func<T, bool>> expression = null,
            Func<System.Linq.IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbSet.AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
