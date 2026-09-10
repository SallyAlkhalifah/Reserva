using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;
using Reserva.Infrastructure.Data;

namespace Reserva.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================================
        // ADD APPOINTMENT
        // =========================================

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        // =========================================
        // SAVE
        // =========================================

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =========================================
        // GET BY EMPLOYEE
        // =========================================

        public async Task<List<Appointment>> GetByEmployeeIdAsync(
            Guid employeeId)
        {
            return await _context.Appointments
                .AsSplitQuery()
                .Include(a => a.Service)
                .Include(a => a.Business)
                .Include(a => a.Customer)
                .Where(a => a.EmployeeId == employeeId)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        // =========================================
        // GET BY BUSINESS
        // =========================================

        public async Task<List<Appointment>> GetByBusinessIdAsync(
            Guid businessId)
        {
            return await _context.Appointments
                .AsSplitQuery()
                .Include(a => a.Service)
                .Include(a => a.Employee)
                    .ThenInclude(e => e!.User)
                .Include(a => a.Business)
                .Include(a => a.Customer)
                .Where(a => a.BusinessId == businessId)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        // =========================================
        // GET BY CUSTOMER
        // =========================================

        public async Task<List<Appointment>> GetByCustomerIdAsync(
            Guid customerId)
        {
            return await _context.Appointments
                .AsSplitQuery()
                .Include(a => a.Service)
                .Include(a => a.Employee)
                    .ThenInclude(e => e!.User)
                .Include(a => a.Business)
                .Include(a => a.Customer)
                .Where(a => a.CustomerId == customerId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        // =========================================
        // GET BY ID
        // =========================================

        public async Task<Appointment?> GetByIdAsync(
            Guid appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Employee)
                    .ThenInclude(e => e!.User)
                .Include(a => a.Business)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }

        // =========================================
        // UPDATE
        // =========================================

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);

            await Task.CompletedTask;
        }
        // =========================================
        // CHECK APPOINTMENT CONFLICT
        // =========================================

        public async Task<bool> HasConflictAsync(
            Guid employeeId,
            DateTime appointmentStart,
            DateTime appointmentEnd)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Service)
                .Where(a =>
                    a.EmployeeId == employeeId &&
                    a.Status != Domain.Enums.AppointmentStatus.Cancelled)
                .ToListAsync();

            return appointments.Any(a =>
            {
                if (a.Service == null)
                    return false;

                var existingStart = a.AppointmentDate;

                var existingEnd = existingStart.AddMinutes(
                    a.Service.DurationInMinutes);

                // =====================================
                // OVERLAP CHECK
                // =====================================

                return appointmentStart < existingEnd &&
                       appointmentEnd > existingStart;
            });
        }

    }
}