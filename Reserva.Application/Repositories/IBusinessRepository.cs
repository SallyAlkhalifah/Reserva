using Reserva.Domain.Entities;

namespace Reserva.Application.Repositories
{
    public interface IBusinessRepository
    {
        Task AddAsync(Business business);

        Task SaveChangesAsync();

        Task<Business?> GetByIdAsync(Guid businessId);

        Task<List<Business>> GetAllAsync();

        Task<List<Business>> GetByOwnerIdAsync(Guid ownerId);
        Task AddServiceAsync(Service service);
    }
}