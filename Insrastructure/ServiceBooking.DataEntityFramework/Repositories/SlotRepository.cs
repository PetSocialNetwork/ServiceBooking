using Microsoft.EntityFrameworkCore;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Interfaces;
using System;

namespace ServiceBooking.DataEntityFramework.Repositories
{
    public class SlotRepository : EFRepository<Slot>, ISlotRepository
    {
        public SlotRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Slot>> GetAvailableSlotsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(s => s.ServiceId == serviceId && s.IsAvailable && s.SlotDateTime >= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsBusySlotsExistsAsync
            (Guid serviceId, CancellationToken cancellationToken)
        {
            return await Entities
                .AnyAsync(s => s.ServiceId == serviceId && !s.IsAvailable, cancellationToken);
        }

        public async Task DeleteAllStoltsByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken)
        {
            var slots = await Entities.Where(s => s.ServiceId == serviceId)
                .ToListAsync(cancellationToken);
            Entities.RemoveRange(slots);
        }
    }
}
