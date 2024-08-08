using Ecommerce.WebApp.Model.Entities;

namespace Ecommerce.WebApp.Model.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale sale);
    }
}
