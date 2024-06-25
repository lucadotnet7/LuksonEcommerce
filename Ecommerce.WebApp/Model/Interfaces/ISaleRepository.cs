using Ecommerce.WebApp.Model.Entities;

namespace Ecommerce.WebApp.Model.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Venta>
    {
        Task<Venta> Register(Venta sale);
    }
}
