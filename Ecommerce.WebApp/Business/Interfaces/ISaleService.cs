using Ecommerce.WebApp.Model.DTOs;

namespace Ecommerce.WebApp.Business.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDto> Register(SaleDto request);
    }
}
