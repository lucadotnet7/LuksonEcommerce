using AutoMapper;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;

namespace Ecommerce.WebApp.Business.Utils.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, SessionDto >();
            CreateMap<UserDto, User>();

            CreateMap<Categoria, CategoryDto>();
            CreateMap<CategoryDto, Categoria>();

            CreateMap<Producto, ProductDto>();
            CreateMap<ProductDto, Producto>()
                .ForMember(x => x.IdCategoriaNavigation, opt => opt.Ignore());

            CreateMap<DetalleVenta, SaleDetailDto>();
            CreateMap<SaleDetailDto, DetalleVenta>();

            CreateMap<Venta, SaleDto>();
            CreateMap<SaleDto, Venta>();
        }
    }
}
