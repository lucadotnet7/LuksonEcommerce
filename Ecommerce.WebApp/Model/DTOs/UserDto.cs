using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class UserDto(
        int UserId, 
        [Required(ErrorMessage = "Ingrese el nombre completo.")] string FullName, 
        [Required(ErrorMessage = "Ingrese el correo electrónico.")] string Email, 
        [Required(ErrorMessage = "Ingrese la contraseña.")] string Password, 
        [Required(ErrorMessage = "Confirme su contraseña.")] string ConfirmPassword, 
        string? Role);
}
