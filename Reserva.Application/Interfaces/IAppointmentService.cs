using System;
using System.Collections.Generic;
using System.Text;

using Reserva.Application.DTOs;

namespace Reserva.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<Guid> CreateAppointmentAsync(
            Guid customerId,
            BookingCartItemDto item);

        Task<List<AppointmentDto>> GetAppointmentsByEmployeeIdAsync(
            Guid employeeId);

        Task<List<AppointmentDto>> GetAppointmentsByBusinessIdAsync(
            Guid businessId);

        Task<List<AppointmentDto>> GetAppointmentsByCustomerIdAsync(
            Guid customerId);

        Task UpdateAppointmentStatusAsync(
            Guid appointmentId,
            string status);
    }
}
