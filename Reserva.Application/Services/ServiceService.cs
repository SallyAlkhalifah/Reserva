using System;
using System.Collections.Generic;
using System.Text;
using Reserva.Application.DTOs;
using Reserva.Application.Interfaces;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;

namespace Reserva.Application.Services
{
    public class ServiceService : IService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateServiceAsync(CreateServiceDto dto)
        {
            var service = new Service
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DurationInMinutes = dto.DurationInMinutes,
                BusinessId = dto.BusinessId
            };

            await _repository.AddAsync(service);
            await _repository.SaveChangesAsync();

            return service.Id;
        }
    }
}