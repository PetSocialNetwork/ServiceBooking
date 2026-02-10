namespace ServiceBooking.Domain.Exceptions
{
    public class BookingNotFoundException : DomainException
    {
        public BookingNotFoundException(string? message) : base(message)
        {
        }

        public BookingNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
