using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Application.DTOs
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}

     