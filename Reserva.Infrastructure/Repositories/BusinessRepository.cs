using Microsoft.EntityFrameworkCore;
using Reserva.Application.Repositories;
using Reserva.Domain.Entities;
using Reserva.Infrastructure.Data;

namespace Reserva.Infrastructure.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Business business)
        {
            await _context.Businesses.AddAsync(business);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Business?> GetByIdAsync(Guid businessId)
        {
            return await _context.Businesses
                .Include(b => b.Services)
                .Include(b => b.Employees)
                    .ThenInclude(e => e.User)
                .Include(b => b.WorkingHours)
                .FirstOrDefaultAsync(b => b.Id == businessId);
        }

        public async Task<List<Business>> GetAllAsync()
        {
            return await _context.Businesses
                .Include(b => b.Services)
                .Include(b => b.Employees)
                    .ThenInclude(e => e.User)
                .Include(b => b.WorkingHours)
                .ToListAsync();
        }

        public async Task<List<Business>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Businesses
                .Include(b => b.Employees)
                    .ThenInclude(e => e.User)
                .Include(b => b.Services)
                .Include(b => b.WorkingHours)
                .Where(b => b.OwnerId == ownerId)
                .ToListAsync();
        }
        public async Task AddServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }
    }
}