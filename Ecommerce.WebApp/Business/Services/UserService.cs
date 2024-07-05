using AutoMapper;
using Azure.Core;
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
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> List(string role, string search)
        {
            try
            {
                IQueryable<Usuario> query = _repository.Get(
                    u => u.Rol == role &&
                    string.Concat(
                        u.NombreCompleto!.ToLower(), u.Correo!.ToLower()
                    )
                    .Contains(search.ToLower()));

                List<UserDto> users = _mapper.Map<List<UserDto>>(await query.ToListAsync());

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
                Usuario? user = await _repository.Get(u => u.IdUsuario == id).FirstOrDefaultAsync();

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
                Usuario? user = await _repository.Get(u => u.Correo == request.Email && u.Clave == request.Password)
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
                Usuario userToAdd = _mapper.Map<Usuario>(request);
                Usuario userAdded = await _repository.Add(userToAdd);

                if (userAdded.IdUsuario != 0)
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
                Usuario? user = await _repository.Get(p => p.IdUsuario == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                    throw new UserException("El usuario que intenta actualizar no existe.");

                user.NombreCompleto = request.FullName;
                user.Correo = request.Email;
                user.Clave = request.Password;

                Usuario updatedUser = await _repository.Update(user);

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
                Usuario? user = await _repository.Get(p => p.IdUsuario == id).FirstOrDefaultAsync();

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
