namespace ServiceBooking.Domain.Exceptions
{
    public class SlotNotFoundException : DomainException
    {
        public SlotNotFoundException(string? message) : base(message)
        {
        }

        public SlotNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
