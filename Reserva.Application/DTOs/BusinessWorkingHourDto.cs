using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Reserva.Application.DTOs
{
    public class BusinessWorkingHourDto
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
}

