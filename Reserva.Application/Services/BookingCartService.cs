using Reserva.Application.DTOs;
using Reserva.Application.Interfaces;

namespace Reserva.Application.Services
{
    public class BookingCartService : IBookingCartService
    {
        private readonly List<BookingCartItemDto> _items = new();

        public IReadOnlyList<BookingCartItemDto> Items => _items;

        public event Action? OnChange;


        public void AddService(BookingCartItemDto item)
        {
            if (ContainsService(item.ServiceId))
                return;

            _items.Add(item);

            OnChange?.Invoke();
        }


        public void RemoveService(Guid serviceId)
        {
            var item = _items.FirstOrDefault(
                x => x.ServiceId == serviceId);

            if (item != null)
            {
                _items.Remove(item);

                OnChange?.Invoke();
            }
        }


        public void Clear()
        {
            _items.Clear();

            OnChange?.Invoke();
        }


        public bool ContainsService(Guid serviceId)
        {
            return _items.Any(
                x => x.ServiceId == serviceId);
        }


        public decimal GetTotal()
        {
            return _items.Sum(x => x.Price);
        }
    }
}