using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Application.DTOs
{
    public class BookingCartItemDto
    {
        public Guid ServiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public Guid BusinessId { get; set; }

        public string BusinessName { get; set; } = string.Empty;

        public string ServiceName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }
}
