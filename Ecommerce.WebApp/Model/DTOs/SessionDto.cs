using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Model.DTOs
{
    public record class SessionDto(
        int UserId,
        string FullName,
        string Email,
        string? Role);
}
