using System;

namespace Reserva.Application.DTOs
{
    public class BusinessDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string? BannerUrl { get; set; }

        public string? GoogleMapsUrl { get; set; }

        public List<EmployeeDto> Employees { get; set; } = new();

        public List<ServiceDto> Services { get; set; } = new();

        public int EmployeeCount => Employees.Count;

        public int ServiceCount => Services.Count;

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        public List<BusinessWorkingHourDto> WorkingHours { get; set; } = new();
    }
}