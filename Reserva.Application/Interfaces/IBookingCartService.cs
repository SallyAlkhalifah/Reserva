using Reserva.Application.DTOs;

namespace Reserva.Application.Interfaces
{
    public interface IBookingCartService
    {

            IReadOnlyList<BookingCartItemDto> Items { get; }

            event Action? OnChange;

            void AddService(BookingCartItemDto item);

            void RemoveService(Guid serviceId);

            void Clear();

            bool ContainsService(Guid serviceId);

            decimal GetTotal();
        
    }
}