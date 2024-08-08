using Ecommerce.WebApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApp.Model;

public partial class EcommerceContext : DbContext
{
    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<SaleDetail> SaleDetail { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }
}
