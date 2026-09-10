using System;
using System.Collections.Generic;
using System.Text;
using Reserva.Application.DTOs;

namespace Reserva.Application.Interfaces
{
    public interface IService
    {
        Task<Guid> CreateServiceAsync(CreateServiceDto dto);
    }
}
