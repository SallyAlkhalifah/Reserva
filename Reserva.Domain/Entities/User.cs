using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Entities
{

    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public DateOnly? DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();

        public ICollection<Business> OwnedBusinesses { get; set; }
            = new List<Business>();

        public ICollection<Employee> EmployeeProfiles { get; set; }
            = new List<Employee>();
    }
}
