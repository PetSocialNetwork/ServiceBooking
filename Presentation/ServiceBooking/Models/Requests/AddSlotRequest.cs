namespace ServiceBooking.WebApi.Models.Requests
{
    public class AddSlotRequest
    {
        public Guid ServiceId { get; set; }
        public DateTime SlotDateTime { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
