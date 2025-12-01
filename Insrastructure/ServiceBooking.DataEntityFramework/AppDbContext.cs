using Microsoft.EntityFrameworkCore;
using ServiceBooking.Domain.Entities;

namespace ServiceBooking.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<Slot> Slots => Set<Slot>();
        DbSet<Booking> Bookings => Set<Booking>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
