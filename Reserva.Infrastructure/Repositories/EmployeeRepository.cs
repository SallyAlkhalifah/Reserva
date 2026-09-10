using Microsoft.EntityFrameworkCore;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;
using Reserva.Infrastructure.Data;

namespace Reserva.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid employeeId)
        {
            return await _context.Employees
                .AsSplitQuery()
                .Include(e => e.User)
                .Include(e => e.Services)
                .Include(e => e.WorkingHours)
                .Include(e => e.Appointments)
                    .ThenInclude(a => a.Service)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<Employee?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Employees
                .AsSplitQuery()
                .Include(e => e.User)
                .Include(e => e.Services)
                .Include(e => e.WorkingHours)
                .Include(e => e.Appointments)
                    .ThenInclude(a => a.Service)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<List<Employee>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _context.Employees
                .AsSplitQuery()
                .Include(e => e.User)
                .Include(e => e.Services)
                .Include(e => e.WorkingHours)
                .Include(e => e.Appointments)
                    .ThenInclude(a => a.Service)
                .Where(e => e.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            return await _context.Services
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task DeleteWorkingHoursAsync(Guid employeeId)
        {
            var workingHours = await _context.EmployeeWorkingHours
                .Where(h => h.EmployeeId == employeeId)
                .ToListAsync();

            if (workingHours.Count > 0)
            {
                _context.EmployeeWorkingHours.RemoveRange(workingHours);
            }
        }

        public async Task DeleteEmployeeServicesAsync(Guid employeeId)
        {
            var employeeServices = await _context
                .Set<Dictionary<string, object>>("EmployeeServices")
                .Where(x => EF.Property<Guid>(x, "EmployeeId") == employeeId)
                .ToListAsync();

            if (employeeServices.Count > 0)
            {
                _context
                    .Set<Dictionary<string, object>>("EmployeeServices")
                    .RemoveRange(employeeServices);
            }
        }

        public async Task AddEmployeeServiceAsync(
            Guid employeeId,
            Guid serviceId)
        {
            var employeeService = new Dictionary<string, object>
            {
                ["EmployeeId"] = employeeId,
                ["ServiceId"] = serviceId
            };

            await _context
                .Set<Dictionary<string, object>>("EmployeeServices")
                .AddAsync(employeeService);
        }

        public async Task AddWorkingHourAsync(EmployeeWorkingHour workingHour)
        {
            await _context.EmployeeWorkingHours.AddAsync(workingHour);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

    }
}