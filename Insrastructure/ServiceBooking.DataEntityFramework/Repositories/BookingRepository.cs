using Microsoft.EntityFrameworkCore;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class BookingRepository : EFRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<Booking>> GetBookingsByServiceId(Guid serviceId, CancellationToken cancellationToken)
        {
            return await Entities
                .OrderByDescending(s => s.BookedAt)
                .Include(s => s.Slot)
                .Where(s => s.ServiceId == serviceId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByProfileId(Guid profileId, CancellationToken cancellationToken)
        {
            return await Entities
                .OrderByDescending(s => s.BookedAt)
                .Include(s => s.Slot)
                .Where(s => s.ProfileId == profileId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> FindBookingById(Guid bookingId, CancellationToken cancellationToken)
        {
            return await Entities
               .Where(s => s.Id == bookingId)
               .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
