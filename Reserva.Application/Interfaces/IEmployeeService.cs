using Reserva.Application.DTOs;

namespace Reserva.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<Guid> CreateEmployeeAsync(CreateEmployeeDto dto);

        Task<List<EmployeeDto>> GetEmployeesByBusinessIdAsync(Guid businessId);

        Task<EmployeeDto?> GetEmployeeByIdAsync(Guid EmployeeId);

        Task<EmployeeDto?> GetEmployeeByUserIdAsync(Guid userId);

        Task UpdateEmployeeAsync(Guid EmployeeId, CreateEmployeeDto dto);
    }
}