using System.Linq.Expressions;

namespace Ecommerce.WebApp.Model.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>>? filter = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
