using Microsoft.EntityFrameworkCore;
using Reserva.Domain.Entities;

namespace Reserva.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<BusinessWorkingHour> BusinessWorkingHours => Set<BusinessWorkingHour>();
    public DbSet<User> Users => Set<User>();

    public DbSet<Business> Businesses => Set<Business>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Service> Services => Set<Service>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    public DbSet<EmployeeWorkingHour> EmployeeWorkingHours => Set<EmployeeWorkingHour>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================================================
        // BUSINESS WORKING HOURS
        // =====================================================

        modelBuilder.Entity<BusinessWorkingHour>()
            .HasOne(w => w.Business)
            .WithMany(b => b.WorkingHours)
            .HasForeignKey(w => w.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);


        // =====================================================
        // SERVICE PRICE
        // =====================================================

        modelBuilder.Entity<Service>()
            .Property(s => s.Price)
            .HasPrecision(10, 2);


        // =====================================================
        // BUSINESS OWNER
        // =====================================================

        modelBuilder.Entity<Business>()
            .HasOne(b => b.Owner)
            .WithMany(u => u.OwnedBusinesses)
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        // =====================================================
        // EMPLOYEE → USER
        // =====================================================

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithMany(u => u.EmployeeProfiles)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        // =====================================================
        // EMPLOYEE ↔ SERVICE
        // MANY-TO-MANY
        // =====================================================

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Services)
            .WithMany(s => s.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeServices",

                right => right
                    .HasOne<Service>()
                    .WithMany()
                    .HasForeignKey("ServiceId")
                    .OnDelete(DeleteBehavior.Restrict),

                left => left
                    .HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade),

                join =>
                {
                    join.HasKey("EmployeeId", "ServiceId");
                });


        // =====================================================
        // APPOINTMENT → CUSTOMER
        // =====================================================

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Customer)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);


        // =====================================================
        // APPOINTMENT → EMPLOYEE
        // =====================================================

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Appointments)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);


        // =====================================================
        // APPOINTMENT → BUSINESS
        // =====================================================

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Business)
            .WithMany(b => b.Appointments)
            .HasForeignKey(a => a.BusinessId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
