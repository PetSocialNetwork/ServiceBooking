using ServiceBooking.Domain.Entities;

namespace ServiceBooking.Domain.Interfaces
{
    public interface IBookingRepository : IRepositoryEF<Booking>
    {
        Task<Booking?> FindBookingById(Guid bookingId, CancellationToken cancellationToken);
        Task<IEnumerable<Booking>> GetBookingsByServiceId(Guid serviceId, CancellationToken cancellationToken);
        Task<IEnumerable<Booking>> GetBookingsByProfileId(Guid profileId, CancellationToken cancellationToken);
    }
}
