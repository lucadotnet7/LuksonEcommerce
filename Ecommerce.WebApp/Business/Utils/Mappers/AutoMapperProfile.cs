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

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.CategoryIdNavigation, opt => opt.Ignore());

            CreateMap<SaleDetail, SaleDetailDto>();
            CreateMap<SaleDetailDto, SaleDetail>();

            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
        }
    }
}
