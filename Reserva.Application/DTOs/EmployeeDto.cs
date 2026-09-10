using System;
using System.Collections.Generic;

namespace Reserva.Application.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BusinessId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public BusinessDto? Business { get; set; }

        public List<ServiceDto> Services { get; set; } = new();

        public List<Guid> ServiceIds { get; set; } = new();

        public List<EmployeeWorkingHourDto> WorkingHours { get; set; } = new();

        public List<AppointmentDto> Appointments { get; set; } = new();
    }
}