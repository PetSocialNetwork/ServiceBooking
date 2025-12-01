using ServiceBooking.Domain.Entities;

namespace ServiceBooking.Domain.Interfaces
{
    public interface IBookingRepository : IRepositoryEF<Booking>
    {
        Task<Booking?> FindBookingAsync(Guid id, CancellationToken cancellationToken);
    }
}
