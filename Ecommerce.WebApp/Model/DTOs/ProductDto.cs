using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class ProductDto(
        int ProductId,
        [Required(ErrorMessage = "Ingrese el nombre del producto.")] string Name,
        [Required(ErrorMessage = "Ingrese la descripción del producto.")] string Description,
        int? CategoryId,
        [Required(ErrorMessage = "Ingrese el precio del producto.")] decimal Price,
        [Required(ErrorMessage = "Ingrese precio de oferta.")] decimal? OfferPrice,
        [Required(ErrorMessage = "Ingrese la cantidad.")] int? Quantity,
        [Required(ErrorMessage = "Ingrese la url de la imagen.")] string? Image,
        CategoryDto IdCategoryNavigation);
}
