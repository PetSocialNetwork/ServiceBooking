using ServiceBooking.Domain.Entities;

namespace ServiceBooking.Domain.Interfaces
{
    public interface ISlotRepository : IRepositoryEF<Slot>
    {
        Task<List<Slot>> GetAvailableSlotsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken);
    }
}
