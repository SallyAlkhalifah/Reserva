using System;

namespace Reserva.Application.DTOs
{
    public class EmployeeWorkingHourDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
}