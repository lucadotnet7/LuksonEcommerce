using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Ecommerce.WebApp.Model.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EcommerceContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(EcommerceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _dbSet : _dbSet.Where(filter);
        }

        public async Task<T> Add(T entity)
        {
            try
            {
               EntityEntry<T> entry = await _dbSet.AddAsync(entity);
                
                await _context.SaveChangesAsync();

                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                EntityEntry<T> entry = _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
