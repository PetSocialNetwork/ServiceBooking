namespace ServiceBooking.Domain.Entities.Enums
{
    public enum BookingStatus
    {
        Pending,    // Ожидание подтверждения
        Confirmed,  // Подтверждено
        Cancelled,  // Отменено
        Completed   // Услуга оказана
    }
}
