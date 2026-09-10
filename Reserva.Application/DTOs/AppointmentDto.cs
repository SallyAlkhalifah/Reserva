using System;

namespace Reserva.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid BusinessId { get; set; }
        public ServiceDto? Service { get; set; }
        public BusinessDto? Business { get; set; }
        public CustomerDto? Customer { get; set; }

    }

}