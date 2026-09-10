using Reserva.Application.DTOs;
using Reserva.Application.Interfaces;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;
using System.Linq;

namespace Reserva.Application.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _repository;

        public BusinessService(IBusinessRepository repository)
        {
            _repository = repository;
        }

        // =========================================
        // CREATE BUSINESS
        // =========================================

        public async Task<Guid> CreateBusinessAsync(CreateBusinessDto dto)
        {
            var business = new Business
            {
                Id = Guid.NewGuid(),

                Name = dto.Name,
                Category = dto.Category,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,

                City = dto.City,
                Address = dto.Address,
                Description = dto.Description,

                LogoUrl = dto.LogoUrl,
                BannerUrl = dto.BannerUrl,

                GoogleMapsUrl = dto.GoogleMapsUrl,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

                OwnerId = dto.OwnerId
            };

            // =========================================
            // WORKING HOURS
            // =========================================

            if (dto.WorkingHours != null && dto.WorkingHours.Any())
            {
                business.WorkingHours = dto.WorkingHours
                    .Select(hour => new BusinessWorkingHour
                    {
                        Id = Guid.NewGuid(),

                        BusinessId = business.Id,

                        DayOfWeek = hour.DayOfWeek,
                        OpenTime = hour.OpenTime,
                        CloseTime = hour.CloseTime,
                        IsClosed = hour.IsClosed
                    })
                    .ToList();
            }

            // =========================================
            // ADD BUSINESS
            // =========================================

            await _repository.AddAsync(business);

            // =========================================
            // SAVE DATABASE
            // =========================================

            await _repository.SaveChangesAsync();

            // =========================================
            // RETURN CREATED BUSINESS ID
            // =========================================

            return business.Id;
        }
        


        // =========================================
        // GET BUSINESS BY ID
        // =========================================

        public async Task<BusinessDto?> GetBusinessByIdAsync(Guid businessId)
        {
            var business =
                await _repository.GetByIdAsync(businessId);

            if (business == null)
            {
                return null;
            }

            return new BusinessDto
            {
                Id = business.Id,
                OwnerId = business.OwnerId,
                Name = business.Name,
                Category = business.Category,

                Email = business.Email,
                PhoneNumber = business.PhoneNumber,

                City = business.City,
                Address = business.Address,

                Description = business.Description,

                LogoUrl = business.LogoUrl,
                BannerUrl = business.BannerUrl,

                GoogleMapsUrl = business.GoogleMapsUrl,

                Latitude = business.Latitude,
                Longitude = business.Longitude,


                // =========================================
                // EMPLOYEES
                // =========================================

                Employees = business.Employees?
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        UserId = e.UserId,

                        FirstName =
                            e.User?.FirstName ?? "",

                        LastName =
                            e.User?.LastName ?? "",

                        JobTitle = e.JobTitle,

                        IsActive = e.IsActive
                    })
                    .ToList()
                    ?? new(),


                // =========================================
                // SERVICES
                // =========================================

                Services = business.Services?
                    .Select(s => new ServiceDto
                    {
                        Id = s.Id,

                        Name = s.Name,

                        Description =
                            s.Description,

                        Price = s.Price,

                        DurationInMinutes =
                            s.DurationInMinutes
                    })
                    .ToList()
                    ?? new(),


                // =========================================
                // WORKING HOURS
                // =========================================

                WorkingHours = business.WorkingHours?
                    .Select(w => new BusinessWorkingHourDto
                    {
                        Id = w.Id,

                        DayOfWeek =
                            w.DayOfWeek,

                        OpenTime =
                            w.OpenTime,

                        CloseTime =
                            w.CloseTime,

                        IsClosed =
                            w.IsClosed
                    })
                    .OrderBy(w => w.DayOfWeek)
                    .ToList()
                    ?? new()
            };
        }


        // =========================================
        // GET ALL BUSINESSES
        // =========================================

        public async Task<List<BusinessDto>> GetAllBusinessesAsync()
        {
            var businesses =
                await _repository.GetAllAsync();

            return businesses
                .Select(b => new BusinessDto
                {
                    Id = b.Id,

                    Name = b.Name,
                    Category = b.Category,

                    Email = b.Email,
                    PhoneNumber = b.PhoneNumber,

                    City = b.City,
                    Address = b.Address,

                    LogoUrl = b.LogoUrl,
                    BannerUrl = b.BannerUrl,

                    Description = b.Description,

                    Latitude = b.Latitude,
                    Longitude = b.Longitude,

                    GoogleMapsUrl =
                        b.GoogleMapsUrl,


                    Employees = b.Employees?
                        .Select(e => new EmployeeDto
                        {
                            Id = e.Id,

                            UserId = e.UserId,

                            FirstName =
                                e.User?.FirstName ?? "",

                            LastName =
                                e.User?.LastName ?? "",

                            JobTitle =
                                e.JobTitle,

                            IsActive =
                                e.IsActive
                        })
                        .ToList()
                        ?? new(),


                    Services = b.Services?
                        .Select(s => new ServiceDto
                        {
                            Id = s.Id,

                            Name = s.Name,

                            Description =
                                s.Description,

                            Price = s.Price,

                            DurationInMinutes =
                                s.DurationInMinutes
                        })
                        .ToList()
                        ?? new()
                })
                .ToList();
        }


        // =========================================
        // GET BUSINESSES BY OWNER
        // =========================================

        public async Task<List<BusinessDto>>
            GetBusinessesByOwnerIdAsync(Guid ownerId)
        {
            var businesses =
                await _repository
                    .GetByOwnerIdAsync(ownerId);

            return businesses
                .Select(b => new BusinessDto
                {
                    Id = b.Id,

                    Name = b.Name,
                    Category = b.Category,

                    Email = b.Email,
                    PhoneNumber = b.PhoneNumber,

                    City = b.City,
                    Address = b.Address,

                    LogoUrl = b.LogoUrl,
                    BannerUrl = b.BannerUrl,

                    Description =
                        b.Description,

                    Latitude =
                        b.Latitude,

                    Longitude =
                        b.Longitude,

                    GoogleMapsUrl =
                        b.GoogleMapsUrl,


                    Employees = b.Employees?
                        .Select(e => new EmployeeDto
                        {
                            Id = e.Id,

                            UserId = e.UserId,

                            FirstName =
                                e.User?.FirstName ?? "",

                            LastName =
                                e.User?.LastName ?? "",

                            JobTitle =
                                e.JobTitle,

                            IsActive =
                                e.IsActive
                        })
                        .ToList()
                        ?? new(),


                    Services = b.Services?
                        .Select(s => new ServiceDto
                        {
                            Id = s.Id,

                            Name = s.Name,

                            Description =
                                s.Description,

                            Price =
                                s.Price,

                            DurationInMinutes =
                                s.DurationInMinutes
                        })
                        .ToList()
                        ?? new()
                })
                .ToList();
        }

        public async Task UpdateBusinessAsync(UpdateBusinessDto dto)
        {
            var business = await _repository.GetByIdAsync(dto.Id);

            if (business == null)
            {
                throw new Exception("Business not found.");
            }

            // =========================================
            // BUSINESS INFORMATION
            // =========================================

            business.Name = dto.Name;
            business.Category = dto.Category;
            business.Email = dto.Email;
            business.PhoneNumber = dto.PhoneNumber;

            business.City = dto.City;
            business.Address = dto.Address;
            business.Description = dto.Description;

            // =========================================
            // BRANDING
            // =========================================

            business.LogoUrl = dto.LogoUrl;
            business.BannerUrl = dto.BannerUrl;

            // =========================================
            // LOCATION
            // =========================================

            business.GoogleMapsUrl = dto.GoogleMapsUrl;
            business.Latitude = dto.Latitude;
            business.Longitude = dto.Longitude;

            // =========================================
            // WORKING HOURS
            // =========================================

            if (dto.WorkingHours != null)
            {
                foreach (var dtoHour in dto.WorkingHours)
                {
                    var existingHour = business.WorkingHours
                        .FirstOrDefault(x =>
                            x.DayOfWeek == dtoHour.DayOfWeek);

                    if (existingHour != null)
                    {
                        // تعديل الـ row الموجود
                        existingHour.OpenTime = dtoHour.OpenTime;
                        existingHour.CloseTime = dtoHour.CloseTime;
                        existingHour.IsClosed = dtoHour.IsClosed;
                    }
                    else
                    {
                        // إذا اليوم غير موجود، أنشئه
                        business.WorkingHours.Add(
                            new BusinessWorkingHour
                            {
                                Id = Guid.NewGuid(),
                                BusinessId = business.Id,
                                DayOfWeek = dtoHour.DayOfWeek,
                                OpenTime = dtoHour.OpenTime,
                                CloseTime = dtoHour.CloseTime,
                                IsClosed = dtoHour.IsClosed
                            });
                    }
                }
            }

            // =========================================
            // SAVE
            // =========================================

            await _repository.SaveChangesAsync();
        }

        public async Task AddServicesAsync(Guid businessId,List<ServiceDto> services)
        {
            foreach (var dto in services)
            {
                var service = new Service
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    DurationInMinutes = dto.DurationInMinutes,
                    BusinessId = businessId
                };

                await _repository.AddServiceAsync(service);
            }

            await _repository.SaveChangesAsync();
        }


    }

}