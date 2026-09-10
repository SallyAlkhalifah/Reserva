using Reserva.Application.DTOs;
using Reserva.Application.Interfaces;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;

namespace Reserva.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // =========================================
        // CREATE EMPLOYEE
        // =========================================

        public async Task<Guid> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            // Create the User first
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName ?? string.Empty,
                Username = dto.Username,
                Password = dto.Password ?? string.Empty,
                Email = $"{dto.Username}@reserva.local",
                PhoneNumber = string.Empty,
                City = string.Empty,
                Role = "Employee"
            };

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                BusinessId = dto.BusinessId,
                JobTitle = dto.JobTitle,
                IsActive = true
            };

            await _repository.AddUserAsync(user);
            await _repository.AddAsync(employee);

            // Assign services
            foreach (var serviceId in dto.ServiceIds.Distinct())
            {
                var service = await _repository.GetServiceByIdAsync(serviceId);

                if (service != null && service.BusinessId == dto.BusinessId)
                {
                    await _repository.AddEmployeeServiceAsync(
                        employee.Id,
                        service.Id);
                }
            }

            // Add working hours
            foreach (var hour in dto.WorkingHours)
            {
                var workingHour = new EmployeeWorkingHour
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DayOfWeek = hour.DayOfWeek,
                    OpenTime = hour.OpenTime,
                    CloseTime = hour.CloseTime,
                    IsClosed = hour.IsClosed
                };

                await _repository.AddWorkingHourAsync(workingHour);
            }

            // Save everything
            await _repository.SaveChangesAsync();

            return employee.Id;
        }


        // =========================================
        // GET EMPLOYEE BY ID
        // =========================================

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid EmployeeId)
        {
            var employee =
                await _repository.GetByIdAsync(EmployeeId);

            if (employee == null)
                return null;

            return MapToDto(employee);
        }


        // =========================================
        // GET EMPLOYEE BY USER ID
        // =========================================

        public async Task<EmployeeDto?> GetEmployeeByUserIdAsync(Guid userId)
        {
            var employee =
                await _repository.GetByUserIdAsync(userId);

            if (employee == null)
                return null;

            return MapToDto(employee);
        }


        // =========================================
        // GET EMPLOYEES BY BUSINESS
        // =========================================

        public async Task<List<EmployeeDto>> GetEmployeesByBusinessIdAsync(
            Guid businessId)
        {
            var employees =
                await _repository.GetByBusinessIdAsync(businessId);

            return employees
                .Select(MapToDto)
                .ToList();
        }


        // =========================================
        // MAP EMPLOYEE → DTO
        // =========================================

        private static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,

                UserId = employee.UserId,

                BusinessId = employee.BusinessId,

                FirstName = employee.User?.FirstName ?? "",

                LastName = employee.User?.LastName ?? "",

                Username = employee.User?.Username ?? "",

                JobTitle = employee.JobTitle,

                IsActive = employee.IsActive,

                Services = employee.Services
                    .Select(s => new ServiceDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Price = s.Price,
                        DurationInMinutes = s.DurationInMinutes
                    })
                    .ToList(),

                ServiceIds = employee.Services
                    .Select(s => s.Id)
                    .ToList(),

                WorkingHours = employee.WorkingHours
                    .Select(h => new EmployeeWorkingHourDto
                    {
                        DayOfWeek = h.DayOfWeek,
                        OpenTime = h.OpenTime,
                        CloseTime = h.CloseTime,
                        IsClosed = h.IsClosed
                    })
                    .ToList(),

                Appointments = employee.Appointments
                    .Select(a => new AppointmentDto
                    {
                        Id = a.Id,
                        AppointmentDate = a.AppointmentDate,
                        Status = a.Status.ToString(),
                        Notes = a.Notes,

                        CustomerId = a.CustomerId,

                        EmployeeId = a.EmployeeId,

                        ServiceId = a.ServiceId,

                        BusinessId = a.BusinessId,

                        Service = a.Service == null
                            ? null
                            : new ServiceDto
                            {
                                Id = a.Service.Id,
                                Name = a.Service.Name,
                                Description = a.Service.Description,
                                Price = a.Service.Price,
                                DurationInMinutes = a.Service.DurationInMinutes
                            }
                    })
                    .ToList()
            };
        }
        public async Task UpdateEmployeeAsync(
    Guid employeeId,
    CreateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(employeeId);

            if (employee == null)
                throw new InvalidOperationException("Employee not found.");

            // ==========================================
            // UPDATE USER
            // ==========================================

            if (employee.User != null)
            {
                employee.User.FirstName = dto.FirstName;
                employee.User.LastName = dto.LastName ?? string.Empty;
                employee.User.Username = dto.Username;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    employee.User.Password = dto.Password;
                }
            }

            // ==========================================
            // UPDATE EMPLOYEE
            // ==========================================

            employee.JobTitle = dto.JobTitle;

            // ==========================================
            // UPDATE SERVICES
            // ==========================================

            await _repository.DeleteEmployeeServicesAsync(employee.Id);

            foreach (var serviceId in dto.ServiceIds.Distinct())
            {
                var service = await _repository.GetServiceByIdAsync(serviceId);

                if (service != null &&
                    service.BusinessId == employee.BusinessId)
                {
                    await _repository.AddEmployeeServiceAsync(
                        employee.Id,
                        service.Id);
                }
            }

            // ==========================================
            // UPDATE WORKING HOURS
            // ==========================================

            await _repository.DeleteWorkingHoursAsync(employee.Id);

            foreach (var hour in dto.WorkingHours)
            {
                var workingHour = new EmployeeWorkingHour
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employee.Id,
                    DayOfWeek = hour.DayOfWeek,
                    OpenTime = hour.OpenTime,
                    CloseTime = hour.CloseTime,
                    IsClosed = hour.IsClosed
                };

                await _repository.AddWorkingHourAsync(workingHour);
            }

            // ==========================================
            // SAVE
            // ==========================================

            await _repository.SaveChangesAsync();
        }


    }
}