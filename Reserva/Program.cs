using Microsoft.EntityFrameworkCore;
using Reserva.Application.Interfaces;
using Reserva.Application.Repositories;
using Reserva.Application.Services;
using Reserva.Components;
using Reserva.Infrastructure.Data;
using Reserva.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// RAZOR COMPONENTS
// =========================================================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// =========================================================
// DATABASE
// =========================================================

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// =========================================================
// BUSINESS
// =========================================================

builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IBusinessService, BusinessService>();


// =========================================================
// USER
// =========================================================

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// =========================================================
// EMPLOYEE
// =========================================================

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


// =========================================================
// SERVICE
// =========================================================

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IService, ServiceService>();


// =========================================================
// CURRENT USER
// =========================================================

builder.Services.AddScoped<CurrentUserService>();


// =========================================================
// BOOKING CART
// =========================================================

builder.Services.AddScoped<IBookingCartService, BookingCartService>();

// =========================================================
// APPOINTMENT  
// =========================================================
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();


var app = builder.Build();


// =========================================================
// HTTP PIPELINE
// =========================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true);

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();