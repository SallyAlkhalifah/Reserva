using System;

namespace Reserva.Application.DTOs
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DurationInMinutes { get; set; }

        public Guid BusinessId { get; set; }
    }
}