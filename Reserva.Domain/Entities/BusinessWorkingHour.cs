using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Entities
{
    public class BusinessWorkingHour
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }

        public Guid BusinessId { get; set; }

        public Business? Business { get; set; }
    }
}