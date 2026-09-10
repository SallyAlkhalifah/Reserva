using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Entities
{
    public class Business
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }

        public User? Owner { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public ICollection<Service> Services { get; set; } = new List<Service>();
        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }

        public string? Description { get; set; }

        public string? GoogleMapsUrl { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        public ICollection<BusinessWorkingHour> WorkingHours { get; set; } = new List<BusinessWorkingHour>();
    }
}