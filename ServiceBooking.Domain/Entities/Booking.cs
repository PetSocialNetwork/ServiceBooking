using ServiceBooking.Domain.Entities.Enums;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.Domain.Entities
{
    public class Booking : IEntity
    {
        public Guid Id { get; init; }
        public Guid SlotId { get; set; } 
        public Guid ServiceId { get; set; }
        public Guid ProfileId { get; set; } 
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    }
}
