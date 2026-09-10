using System;
using System.Collections.Generic;
using System.Text;
using Reserva.Application.DTOs;
using Reserva.Application.Interfaces;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;
using Reserva.Domain.Enums;

namespace Reserva.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // =========================================
        // CREATE APPOINTMENT
        // =========================================

        // =========================================
        // CREATE APPOINTMENT
        // =========================================

        public async Task<Guid> CreateAppointmentAsync(
            Guid customerId,
            BookingCartItemDto item)
        {
            // =========================================
            // VALIDATE APPOINTMENT TIME
            // =========================================

            var appointmentStart = item.AppointmentDate;

            var appointmentEnd = appointmentStart.AddMinutes(
                item.DurationInMinutes);


            // =========================================
            // CHECK FOR CONFLICT
            // =========================================

            var hasConflict =
                await _repository.HasConflictAsync(
                    item.EmployeeId,
                    appointmentStart,
                    appointmentEnd);


            if (hasConflict)
            {
                throw new InvalidOperationException(
                    $"The selected time is no longer available for {item.EmployeeName}. Please choose another time.");
            }


            // =========================================
            // CREATE APPOINTMENT
            // =========================================

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),

                AppointmentDate = appointmentStart,

                Status = AppointmentStatus.Pending,

                CustomerId = customerId,

                EmployeeId = item.EmployeeId,

                ServiceId = item.ServiceId,

                BusinessId = item.BusinessId
            };


            // =========================================
            // SAVE
            // =========================================

            await _repository.AddAsync(appointment);

            await _repository.SaveChangesAsync();


            return appointment.Id;
        }

        // =========================================
        // GET EMPLOYEE APPOINTMENTS
        // =========================================

        public async Task<List<AppointmentDto>>
            GetAppointmentsByEmployeeIdAsync(Guid employeeId)
        {
            var appointments =
                await _repository.GetByEmployeeIdAsync(employeeId);

            return appointments.Select(MapToDto).ToList();
        }

        // =========================================
        // GET BUSINESS APPOINTMENTS
        // =========================================

        public async Task<List<AppointmentDto>>
            GetAppointmentsByBusinessIdAsync(Guid businessId)
        {
            var appointments =
                await _repository.GetByBusinessIdAsync(businessId);

            return appointments.Select(MapToDto).ToList();
        }

        // =========================================
        // GET CUSTOMER APPOINTMENTS
        // =========================================

        public async Task<List<AppointmentDto>>
            GetAppointmentsByCustomerIdAsync(Guid customerId)
        {
            var appointments =
                await _repository.GetByCustomerIdAsync(customerId);

            return appointments.Select(MapToDto).ToList();
        }

        // =========================================
        // UPDATE STATUS
        // =========================================

        public async Task UpdateAppointmentStatusAsync(
            Guid appointmentId,
            string status)
        {
            var appointment =
                await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            if (!Enum.TryParse<AppointmentStatus>(
                    status,
                    true,
                    out var parsedStatus))
            {
                throw new Exception("Invalid appointment status.");
            }

            appointment.Status = parsedStatus;

            await _repository.UpdateAsync(appointment);

            await _repository.SaveChangesAsync();
        }

        // =========================================
        // MAP TO DTO
        // =========================================

        private AppointmentDto MapToDto(
            Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,

                AppointmentDate =
                    appointment.AppointmentDate,

                Status =
                    appointment.Status.ToString(),

                Notes =
                    appointment.Notes,

                CustomerId =
                    appointment.CustomerId,

                EmployeeId =
                    appointment.EmployeeId,

                ServiceId =
                    appointment.ServiceId,

                BusinessId =
                    appointment.BusinessId,

                Service = appointment.Service == null
                    ? null
                    : new ServiceDto
                    {
                        Id = appointment.Service.Id,
                        Name = appointment.Service.Name,
                        Description = appointment.Service.Description,
                        Price = appointment.Service.Price,
                        DurationInMinutes =
                            appointment.Service.DurationInMinutes
                    },

                Business = appointment.Business == null
                    ? null
                    : new BusinessDto
                    {
                        Id = appointment.Business.Id,
                        Name = appointment.Business.Name,
                        Category = appointment.Business.Category,
                        City = appointment.Business.City,
                        Address = appointment.Business.Address
                    },

                Customer = appointment.Customer == null
                    ? null
                    : new CustomerDto
                    {
                        Id = appointment.Customer.Id,
                        FirstName = appointment.Customer.FirstName,
                        LastName = appointment.Customer.LastName
                    }
            };
        }
    }
}