using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Application.Features.Businesses.CreateBusiness
{
    public class CreateBusinessDto
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
    }
}
