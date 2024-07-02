using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class LoginDto(
        [Required(ErrorMessage = "Ingrese el correo electrónico.")] string Email,
        [Required(ErrorMessage = "Ingrese la contraseña.")] string Password);
}
