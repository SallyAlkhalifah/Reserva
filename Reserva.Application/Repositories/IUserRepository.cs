using Reserva.Domain.Entities;

namespace Reserva.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> LoginAsync(string email, string password);

        Task AddAsync(User user);

        Task SaveChangesAsync();
        Task<User?> GetByUsernameAsync(string username);
    }
}