using AutoMapper;
using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Exceptions;
using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApp.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        
        public CategoryService(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> List(string search)
        {
            try
            {
                IQueryable<Category> query = _repository.Get(c =>
                        c.Name!.ToLower()
                        .Contains(search.ToLower())
                );

                List<CategoryDto> categories = _mapper.Map<List<CategoryDto>>(await query.ToListAsync());

                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            try
            {
                Category? category = await _repository.Get(c => c.Id == id).FirstOrDefaultAsync();

                if (category == null)
                    throw new CategoryException("La categoría que intenta acceder no existe.");

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryDto> Create(CategoryDto request)
        {
            try
            {
                Category categoryToAdd = _mapper.Map<Category>(request);
                Category categoryAdded = await _repository.Add(categoryToAdd);

                if (categoryAdded.Id != 0)
                    return _mapper.Map<CategoryDto>(categoryAdded);
                else
                    throw new CategoryException("La categoría no pudo ser creada.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Edit(CategoryDto request)
        {
            try
            {
                Category? category = await _repository.Get(c => c.Id == request.CategoryId).FirstOrDefaultAsync();

                if (category == null)
                    throw new CategoryException("La categoría que intenta actualizar no existe.");

                category.Name = request.Name;

                Category updatedCategory = await _repository.Update(category);

                if (updatedCategory == null)
                    throw new CategoryException("Ha ocurrido un error al intentar actualizar la categoría.");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Category? category = await _repository.Get(c => c.Id == id).FirstOrDefaultAsync();

                if (category == null)
                    throw new CategoryException("La categoría que intenta eliminar no existe.");

                bool wasDeleted = await _repository.Delete(category);

                if (!wasDeleted)
                    throw new CategoryException("Ha ocurrido un error intentando eliminar la categoría.");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
