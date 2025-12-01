namespace ServiceBooking.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBookingRepository BookingRepository { get; }
        ISlotRepository SlotRepository { get; }
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
