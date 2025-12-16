using ServiceBooking.Domain.Entities.Enums;

namespace ServiceBooking.WebApi.Models.Responses
{
    public class BookingResponse
    {
        public Guid Id { get; init; }
        public Guid SlotId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ProfileId { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime BookedAt { get; set; }
    }
}
