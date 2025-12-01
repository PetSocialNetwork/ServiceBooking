namespace ServiceBooking.WebApi.Models.Responses
{
    public class SlotReponse
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; set; }
        public DateTime SlotDateTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; } 
    }
}
