namespace Ecommerce.WebApp.Model.DTOs
{
    public record class ResponseDto<T>(T? Result, bool IsSuccess, string Message) where T : class;
}
