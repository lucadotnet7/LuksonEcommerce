using Ecommerce.WebApp.Model.DTOs;

namespace Ecommerce.WebApp.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> List(string search);
        Task<CategoryDto> GetById(int id);
        Task<CategoryDto> Create(CategoryDto request);
        Task<bool> Edit(CategoryDto request);
        Task<bool> Delete(int id);
    }
}
