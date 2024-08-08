namespace Ecommerce.WebApp.Model.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Price { get; set; }

    public decimal? OfferPrice { get; set; }

    public int? Quantity { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<SaleDetail> SaleDetail { get; set; } = new List<SaleDetail>();

    public virtual Category? CategoryIdNavigation { get; set; }
}
