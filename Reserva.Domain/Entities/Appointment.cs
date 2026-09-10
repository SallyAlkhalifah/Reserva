using System;
using System.Collections.Generic;
using System.Text;

using Reserva.Domain.Enums;

namespace Reserva.Domain.Entities
{

    public class Appointment
    {
        public Guid Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; }

        public string? Notes { get; set; }

        public Guid CustomerId { get; set; }
        public User? Customer { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }

        public Guid BusinessId { get; set; }
        public Business? Business { get; set; }
    }
}