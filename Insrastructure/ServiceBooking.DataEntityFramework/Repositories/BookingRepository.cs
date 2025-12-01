using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class BookingRepository : EFRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public Task<Booking?> FindBookingAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
