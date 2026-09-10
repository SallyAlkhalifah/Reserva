using Reserva.Application.DTOs;
using Reserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;



namespace Reserva.Application.Interfaces
{
    public interface IBusinessService
    {
        Task<Guid> CreateBusinessAsync(CreateBusinessDto dto);
        Task AddServicesAsync(Guid businessId,List<ServiceDto> services);

        Task<BusinessDto?> GetBusinessByIdAsync(Guid businessId);

        Task<List<BusinessDto>> GetAllBusinessesAsync();

        Task<List<BusinessDto>> GetBusinessesByOwnerIdAsync(Guid ownerId);

        Task UpdateBusinessAsync(UpdateBusinessDto dto);
    }
}