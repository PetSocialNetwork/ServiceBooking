using Microsoft.EntityFrameworkCore;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class SlotRepository : EFRepository<Slot>, ISlotRepository
    {
        public SlotRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Slot>> GetAvailableSlotsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(s => s.ServiceId == serviceId && s.IsAvailable)
                .ToListAsync(cancellationToken);
        }
    }
}
