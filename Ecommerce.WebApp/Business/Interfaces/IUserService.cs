using Ecommerce.WebApp.Model.DTOs;
using ROP;

namespace Ecommerce.WebApp.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> List(string role, string search);
        Task<Result<UserDto>> GetById(int id);
        Task<SessionDto> Authorization(LoginDto request);
        Task<UserDto> Create(UserDto request);
        Task<bool> Edit(UserDto request);
        Task<bool> Delete(int id);
    }
}
