namespace Ecommerce.WebApp.Model.Entities;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int? SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Total { get; set; }

    public virtual Product? ProductIdNavigation { get; set; }

    public virtual Sale? SaleIdNavigation { get; set; }
}
