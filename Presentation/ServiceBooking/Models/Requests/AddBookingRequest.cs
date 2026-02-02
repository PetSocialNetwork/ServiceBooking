#pragma warning disable CS8618 

namespace ServiceBooking.WebApi.Models.Requests
{
    public class AddBookingRequest
    {
        public Guid SlotId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ProfileId { get; set; }
        public string Address { get; set; }
    }
}
