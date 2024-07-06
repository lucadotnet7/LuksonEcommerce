using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Business.Services;
using Ecommerce.WebApp.Business.Utils.Mappers;
using Ecommerce.WebApp.Model;
using Ecommerce.WebApp.Model.Interfaces;
using Ecommerce.WebApp.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void EcommerceServices(this IServiceCollection services)
        {
            services.DbConnection();
            services.EcommerceInjectionDependency();
            services.RegisterAutoMapper();
            services.AddSweetAlert();
            services.AddToast();
        }

        private static void DbConnection(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (IServiceScope scope = services.BuildServiceProvider().CreateScope())
            {
                configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            }

            services.AddDbContext<EcommerceContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        private static void EcommerceInjectionDependency(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        private static void AddSweetAlert(this IServiceCollection services)
        {
            services.AddSweetAlert2();
        }

        private static void AddToast(this IServiceCollection services)
        {
            services.AddBlazoredToast();
        }
    }
}
