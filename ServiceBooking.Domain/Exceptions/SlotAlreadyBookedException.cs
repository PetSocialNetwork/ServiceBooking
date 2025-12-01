namespace ServiceBooking.Domain.Exceptions
{
    public class SlotAlreadyBookedException : DomainException
    {
        public SlotAlreadyBookedException(string? message) : base(message)
        {
        }

        public SlotAlreadyBookedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
