using Ecommerce.WebApp.Model.DTOs;

namespace Ecommerce.WebApp.Business.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> List(string search);
        Task<List<ProductDto>> Catalog(string category, string search);
        Task<ProductDto> GetById(int id);
        Task<ProductDto> Create(ProductDto request);
        Task<bool> Edit(ProductDto request);
        Task<bool> Delete(int id);
    }
}
