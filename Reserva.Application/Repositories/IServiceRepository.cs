using System;
using System.Collections.Generic;
using System.Text;

using Reserva.Domain.Entities;

namespace Reserva.Application.Repositories
{
    public interface IServiceRepository
    {
        Task AddAsync(Service service);
        Task SaveChangesAsync();
    }
}