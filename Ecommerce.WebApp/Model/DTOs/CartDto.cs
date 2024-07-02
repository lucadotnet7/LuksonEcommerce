namespace Ecommerce.WebApp.Model.DTOs
{
    public record class CartDto(ProductDto? Product, int? Quantity, decimal? Price, decimal? TotalPrice);
}
