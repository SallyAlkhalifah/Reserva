using Reserva.Application.DTOs;
namespace Reserva.Application.Services { 
    public class CurrentUserService { 
        public UserDto? User { get; private set; } 
        public DateTime LastActivity { get; private set; } = DateTime.UtcNow; 
        public event Action? OnChange; 
        public bool IsLoggedIn => User != null; 
        public void SetUser(UserDto user) { User = user; LastActivity = DateTime.UtcNow; OnChange?.Invoke(); } 
        public void UpdateActivity() { if (User == null) return; LastActivity = DateTime.UtcNow; } 
        public bool IsSessionExpired() { if (User == null) return true; return DateTime.UtcNow - LastActivity >= TimeSpan.FromMinutes(15); } 
        public void Logout() { User = null; LastActivity = DateTime.UtcNow; OnChange?.Invoke(); } 
    } 
}