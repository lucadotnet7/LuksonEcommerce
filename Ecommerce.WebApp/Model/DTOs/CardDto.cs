using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class CardDto(
        [Required(ErrorMessage = "Ingrese el títular de la tarjeta.")] string? Cardholder, 
        [Required(ErrorMessage = "Ingrese el número de la tarjeta.")] string? CardNumber, 
        [Required(ErrorMessage = "Ingrese la fecha de vigencia de la tarjeta.")] string? Vigency, 
        [Required(ErrorMessage = "Ingrese el código de seguridad de la tarjeta.")] string? CVV);
}
