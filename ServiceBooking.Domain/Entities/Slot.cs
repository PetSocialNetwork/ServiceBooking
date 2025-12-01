using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.Domain.Entities
{
    public class Slot : IEntity
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; set; }
        public DateTime SlotDateTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
