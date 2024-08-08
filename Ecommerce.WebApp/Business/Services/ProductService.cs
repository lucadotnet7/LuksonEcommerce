using AutoMapper;
using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Exceptions;
using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> List(string search)
        {
            try
            {
                IQueryable<Product> query = _repository.Get(p =>
                    p.Name!.ToLower().Contains(search.ToLower()))
                    .Include(c => c.CategoryIdNavigation);

                List<ProductDto> products = _mapper.Map<List<ProductDto>>(await query.ToListAsync());

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductDto>> Catalog(string category, string search)
        {
            try
            {
                IQueryable<Product> query = _repository.Get(p =>
                    p.Name!.ToLower().Contains(search.ToLower()) &&
                    p.CategoryIdNavigation!.Name!.ToLower().Contains(category));

                List<ProductDto> products = _mapper.Map<List<ProductDto>>(await query.ToListAsync());

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> GetById(int id)
        {
            try
            {
                Product? product = await _repository.Get(p => p.Id == id)
                    .Include(c => c.CategoryIdNavigation)
                    .FirstOrDefaultAsync();

                if (product == null)
                    throw new ProductException("El producto que intenta acceder no existe.");

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> Create(ProductDto request)
        {
            try
            {
                Product productToAdd = _mapper.Map<Product>(request);
                Product productAdded = await _repository.Add(productToAdd);

                if (productAdded.Id != 0)
                    return _mapper.Map<ProductDto>(productAdded);
                else
                    throw new ProductException("El producto no pudo ser creada.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Edit(ProductDto request)
        {
            try
            {
                Product? product = await _repository.Get(p => p.Id == request.ProductId).FirstOrDefaultAsync();

                if (product == null)
                    throw new ProductException("El producto que intenta actualizar no existe.");

                product.Name = request.Name;
                product.Description = request.Description;
                product.CategoryId = request.CategoryId;
                product.Price = request.Price;
                product.OfferPrice = request.OfferPrice;
                product.Quantity = request.Quantity;
                product.Image = request.Image;

                Product updatedProduct = await _repository.Update(product);

                if (updatedProduct == null)
                    throw new ProductException("Ha ocurrido un error al intentar actualizar el producto.");

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
                Product? product = await _repository.Get(p => p.Id == id).FirstOrDefaultAsync();

                if (product == null)
                    throw new ProductException("El producto que intenta eliminar no existe.");

                bool wasDeleted = await _repository.Delete(product);

                if (!wasDeleted)
                    throw new ProductException("Ha ocurrido un error intentando eliminar el producto.");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
