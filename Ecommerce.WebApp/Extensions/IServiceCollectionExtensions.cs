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
        }
    }
}
