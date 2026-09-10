using System;

namespace Reserva.Domain.Entities
{
    public class EmployeeWorkingHour
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}