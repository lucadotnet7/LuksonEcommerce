using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class UserDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre completo.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese el correo electrónico.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese la contraseña.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme su contraseña.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        public string? Role { get; set; }
    }
}
