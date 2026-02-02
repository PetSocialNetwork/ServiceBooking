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
                .Where(s => s.ServiceId == serviceId)
                .OrderByDescending(s => s.BookedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByProfileId(Guid profileId, CancellationToken cancellationToken)
        {
            return await Entities.Where(s => s.ProfileId == profileId).ToListAsync(cancellationToken);
        }
    }
}
