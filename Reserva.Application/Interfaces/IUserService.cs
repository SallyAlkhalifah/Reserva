using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reserva.Application.DTOs;

namespace Reserva.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserDto dto);
        Task<UserDto?> GetUserByIdAsync(Guid userId);

        Task<UserDto?> LoginAsync(string login, string password);

    }
}
