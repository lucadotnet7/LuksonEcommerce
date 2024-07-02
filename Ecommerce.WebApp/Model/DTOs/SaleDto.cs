namespace Ecommerce.WebApp.Model.DTOs
{
    public record class SaleDto(
        int IdVenta,
        int? IdUsuario,
        decimal? Total,
        ICollection<SaleDetailDto> SaleDetail);
}
