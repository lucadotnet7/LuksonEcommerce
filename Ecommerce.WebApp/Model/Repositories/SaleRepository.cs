using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.WebApp.Model.Repositories
{
    public class SaleRepository : GenericRepository<Venta>, ISaleRepository
    {
        public SaleRepository(EcommerceContext context) : base(context)
        {       
        }

        public async Task<Venta> Register(Venta sale)
        {
            Venta generatedSale = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta saleDetail in sale.DetalleVenta)
                    {
                        Producto product = _context.Productos.Where(p => p.IdProducto == saleDetail.IdProducto).First();

                        product.Cantidad -= saleDetail.Cantidad;
                        _context.Productos.Update(product);
                    }

                    await _context.SaveChangesAsync();
                    await _dbSet.AddAsync(sale);

                    await _context.SaveChangesAsync();

                    generatedSale = sale;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return generatedSale;
        }
    }
}
