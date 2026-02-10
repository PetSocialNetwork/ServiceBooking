namespace ServiceBooking.Domain.Exceptions
{
    public class BookingInvalidDeleteException : DomainException
    {
        public BookingInvalidDeleteException(string? message) : base(message)
        {
        }

        public BookingInvalidDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
