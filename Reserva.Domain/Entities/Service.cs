using System;
using System.Collections.Generic;

namespace Reserva.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DurationInMinutes { get; set; }

        public Guid BusinessId { get; set; }

        public Business? Business { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();

        // Employees who can provide this service
        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}