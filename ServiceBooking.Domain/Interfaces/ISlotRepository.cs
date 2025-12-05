using ServiceBooking.Domain.Entities;

namespace ServiceBooking.Domain.Interfaces
{
    public interface ISlotRepository : IRepositoryEF<Slot>
    {
        Task<List<Slot>> GetAvailableSlotsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken);
        Task<bool> IsBusySlotsExistsAsync
            (Guid serviceId, CancellationToken cancellationToken);
        Task DeleteAllStoltsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken);
    }
}
