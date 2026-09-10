using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reserva.Application.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        public string? Password { get; set; }
        public string JobTitle { get; set; } = string.Empty;

        // Selected services
        public List<Guid> ServiceIds { get; set; } = new();

        // Working hours
        public List<EmployeeWorkingHourDto> WorkingHours { get; set; }
            = new();
    }
}