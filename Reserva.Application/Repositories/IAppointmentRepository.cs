using Reserva.Domain.Entities;

namespace Reserva.Application.Repositories
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);

        Task SaveChangesAsync();

        Task<List<Appointment>> GetByEmployeeIdAsync(Guid employeeId);

        Task<List<Appointment>> GetByBusinessIdAsync(Guid businessId);

        Task<List<Appointment>> GetByCustomerIdAsync(Guid customerId);

        Task<Appointment?> GetByIdAsync(Guid appointmentId);

        Task UpdateAsync(Appointment appointment);

        Task<bool> HasConflictAsync(
            Guid employeeId,
            DateTime appointmentStart,
            DateTime appointmentEnd);
    }
}