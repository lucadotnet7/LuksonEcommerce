using AutoMapper;
using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Exceptions;
using Ecommerce.WebApp.Model.Interfaces;

namespace Ecommerce.WebApp.Business.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        public SaleService(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Register(SaleDto request)
        {
            try
            {
                Sale sale = _mapper.Map<Sale>(request);
                Sale generatedSale = await _repository.Register(sale);

                if (generatedSale.Id == 0)
                    throw new SaleException("No se pudo registrar la venta.");

                return _mapper.Map<SaleDto>(generatedSale);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
