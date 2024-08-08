using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.WebApp.Model.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(EcommerceContext context) : base(context)
        {       
        }

        public async Task<Sale> Register(Sale sale)
        {
            Sale generatedSale = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (SaleDetail saleDetail in sale.SaleDetail)
                    {
                        Product product = _context.Products.Where(p => p.Id == saleDetail.ProductId).First();

                        product.Quantity -= saleDetail.Quantity;
                        _context.Products.Update(product);
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
