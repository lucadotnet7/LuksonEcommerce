namespace Ecommerce.WebApp.Model.Entities;

public partial class Sale
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<SaleDetail> SaleDetail { get; set; } = new List<SaleDetail>();

    public virtual User? UserIdNavigation { get; set; }
}
