using Reserva.Domain.Entities;

namespace Reserva.Application.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();

        Task<Employee?> GetByIdAsync(Guid employeeId);

        Task<Employee?> GetByUserIdAsync(Guid userId);

        Task<List<Employee>> GetByBusinessIdAsync(Guid businessId);

        Task UpdateAsync(Employee employee);

        Task<Service?> GetServiceByIdAsync(Guid serviceId);

        Task DeleteWorkingHoursAsync(Guid employeeId);

        Task DeleteEmployeeServicesAsync(Guid employeeId);

        Task AddEmployeeServiceAsync(Guid employeeId, Guid serviceId);

        Task AddWorkingHourAsync(EmployeeWorkingHour workingHour);
    }
}