using AutoMapper;
using Ecommerce.WebApp.Business.Interfaces;
using Ecommerce.WebApp.Model.DTOs;
using Ecommerce.WebApp.Model.Entities;
using Ecommerce.WebApp.Model.Exceptions;
using Ecommerce.WebApp.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> List(string role, string search)
        {
            try
            {
                IQueryable<User> query = _repository.Get(
                    u => u.Role == role &&
                    string.Concat(
                        u.FullName!.ToLower(), u.Email!.ToLower()
                    )
                    .Contains(search.ToLower()));

                List<User> usersDb = await query.ToListAsync();

                List<UserDto> users = _mapper.Map<List<UserDto>>(usersDb);

                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<UserDto> GetById(int id)
        {
            try
            {
                User? user = await _repository.Get(u => u.UserId == id).FirstOrDefaultAsync();

                if (user == null)
                    throw new UserException("El usuario que intenta acceder no existe.");
                
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<SessionDto> Authorization(LoginDto request)
        {
            try
            {
                User? user = await _repository.Get(u => u.Email == request.Email && u.Password == request.Password)
                                        .FirstOrDefaultAsync();

                if (user != null)
                    return _mapper.Map<SessionDto>(user);
                else
                    throw new UserException("Los datos ingresados no son válidos.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<UserDto> Create(UserDto request)
        {
            try
            {
                User userToAdd = _mapper.Map<User>(request);
                User userAdded = await _repository.Add(userToAdd);

                if (userAdded.UserId != 0)
                    return _mapper.Map<UserDto>(userAdded);
                else
                    throw new UserException("El usuario no pudo ser creado.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Edit(UserDto request)
        {
            try
            {
                User? user = await _repository.Get(p => p.UserId == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                    throw new UserException("El usuario que intenta actualizar no existe.");

                user.FullName = request.FullName;
                user.Email = request.Email;
                user.Password = request.Password;

                User updatedUser = await _repository.Update(user);

                if (updatedUser == null)
                    throw new UserException("Ha ocurrido un error al intentar actualizar el usuario.");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> Delete(int id)
        {
            try
            {
                User? user = await _repository.Get(p => p.UserId == id).FirstOrDefaultAsync();

                if (user == null)
                    throw new UserException("El usuario que intenta eliminar no existe.");

                bool wasDeleted = await _repository.Delete(user);

                if (!wasDeleted)
                    throw new UserException("Ha ocurrido un error intentando eliminar el usuario.");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
