using ServiceBooking.Domain.Entities.Enums;

namespace ServiceBooking.WebApi.Models.Requests
{
    public class UpdateBookingStatusRequest
    {
        public Guid BookingId { get; set; }
        public BookingStatus Status { get; set; }
    }
}
