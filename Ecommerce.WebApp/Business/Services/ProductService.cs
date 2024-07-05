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
        private readonly IGenericRepository<Producto> _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Producto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> List(string search)
        {
            try
            {
                IQueryable<Producto> query = _repository.Get(p =>
                    p.Nombre!.ToLower().Contains(search.ToLower()))
                    .Include(c => c.IdCategoriaNavigation);

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
                IQueryable<Producto> query = _repository.Get(p =>
                    p.Nombre.ToLower().Contains(search.ToLower()) &&
                    p.IdCategoriaNavigation!.Nombre!.ToLower().Contains(category));

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
                Producto? product = await _repository.Get(p => p.IdProducto == id)
                    .Include(c => c.IdCategoriaNavigation)
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
                Producto productToAdd = _mapper.Map<Producto>(request);
                Producto productAdded = await _repository.Add(productToAdd);

                if (productAdded.IdProducto != 0)
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
                Producto? product = await _repository.Get(p => p.IdProducto == request.ProductId).FirstOrDefaultAsync();

                if (product == null)
                    throw new ProductException("El producto que intenta actualizar no existe.");

                product.Nombre = request.Name;
                product.Descripcion = request.Description;
                product.IdCategoria = request.CategoryId;
                product.Precio = request.Price;
                product.PrecioOferta = request.OfferPrice;
                product.Cantidad = request.Quantity;
                product.Imagen = request.Image;

                Producto updatedProduct = await _repository.Update(product);

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
                Producto? product = await _repository.Get(p => p.IdProducto == id).FirstOrDefaultAsync();

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
