using Microsoft.EntityFrameworkCore.Storage;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _transaction;
        public IBookingRepository BookingRepository { get;  }
        public ISlotRepository SlotRepository { get; }

        public UnitOfWork(AppDbContext dbContext,
            IBookingRepository bookingRepository,
            ISlotRepository slotRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            SlotRepository = slotRepository
                ?? throw new ArgumentNullException(nameof(slotRepository));
            BookingRepository = bookingRepository 
                ?? throw new ArgumentNullException(nameof(bookingRepository));
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
                 => _dbContext.SaveChangesAsync(cancellationToken);
    }
}
