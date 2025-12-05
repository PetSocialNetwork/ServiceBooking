namespace ServiceBooking.WebApi.Models.Requests
{
    public class AddSlotRequest
    {
        public DateTime SlotDateTime { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
