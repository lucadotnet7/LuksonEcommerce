using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Model.Constants;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Interfaces;

namespace Ecommerce.WebApp.Business.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Usuario> _userRepository;
        private readonly IGenericRepository<Producto> _productRepository;

        public DashboardService(
            ISaleRepository saleRepository, IGenericRepository<Usuario> userRepository, 
            IGenericRepository<Producto> productRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public DashboardDto Summary()
        {
            try
            {
                DashboardDto dashbord = new DashboardDto(Revenue(), Sales(), Clients(), Products());

                return dashbord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Revenue()
        {
            IQueryable<Venta> salesQuery = _saleRepository.Get();
            decimal? revenues = salesQuery.Sum(s => s.Total);

            return revenues != null ? revenues.ToString()! : "0";
        }

        private int Sales()
        {
            IQueryable<Venta> salesQuery = _saleRepository.Get();
            int sales = salesQuery.Count();

            return sales;
        }

        private int Clients()
        {
            IQueryable<Usuario> usersQuery = _userRepository.Get(u => u.Rol.ToLower() == UserRoles.Client);
            int clients = usersQuery.Count();

            return clients;
        }

        private int Products()
        {
            IQueryable<Producto> productsQuery = _productRepository.Get();
            int products = productsQuery.Count();

            return products;
        }
    }
}
