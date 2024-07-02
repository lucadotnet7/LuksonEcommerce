namespace Ecommerce.WebApp.Model.DTOs
{
    public record class SaleDetailDto(
        int SaleDetailId,
        int? ProductId,
        int? Quantity,
        decimal? Total);
}
