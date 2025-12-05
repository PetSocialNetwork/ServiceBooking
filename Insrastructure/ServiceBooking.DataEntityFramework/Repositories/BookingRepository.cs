using Microsoft.EntityFrameworkCore;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class BookingRepository : EFRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
