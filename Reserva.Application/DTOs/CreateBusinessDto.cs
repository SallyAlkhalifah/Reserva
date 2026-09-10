using System;
using System.Text;
using System.Collections.Generic;
namespace Reserva.Application.DTOs
{

    public class CreateBusinessDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        public string? BannerUrl { get; set; }

        public string? GoogleMapsUrl { get; set; }
        public List<BusinessWorkingHourDto> WorkingHours { get; set; } = new();


    }
}