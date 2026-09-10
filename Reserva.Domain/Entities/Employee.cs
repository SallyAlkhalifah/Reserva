using System;
using System.Collections.Generic;

namespace Reserva.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }


        // Login account
        public Guid UserId { get; set; }

        public User? User { get; set; }

        // Employee information
        public string JobTitle { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Business
        public Guid BusinessId { get; set; }

        public Business? Business { get; set; }

        // Services this employee can provide
        public ICollection<Service> Services { get; set; }
            = new List<Service>();

        // Appointments
        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();

        // Working Hours
        public ICollection<EmployeeWorkingHour> WorkingHours { get; set; }
            = new List<EmployeeWorkingHour>();

    }
}