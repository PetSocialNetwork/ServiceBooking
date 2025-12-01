using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Exceptions;
using ServiceBooking.Domain.Interfaces;

namespace ServiceBooking.Domain.Services
{
    public class BookingService
    {
        private readonly IUnitOfWork _uow;
        public BookingService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentException(nameof(uow));
        }

        public async Task<List<Slot>> GetAvailableSlotsByServiceIdAsync
            (Guid serviceId, CancellationToken cancellationToken)
        {
            return await _uow.SlotRepository
                .GetAvailableSlotsByServiceIdAsync(serviceId, cancellationToken);
        }

        public async Task AddBookingAsync(Booking booking, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                var slot = await _uow.SlotRepository.GetById(booking.SlotId, cancellationToken)
                    ?? throw new SlotNotFoundException("Свободная дата не найдена.");

                if (!slot.IsAvailable)
                    throw new SlotAlreadyBookedException("Выбранная дата недоступна.");

                slot.IsAvailable = false;
                await _uow.SlotRepository.Update(slot, cancellationToken);
                await _uow.BookingRepository.Add(booking, cancellationToken);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _uow.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task UpdateSlotsAsync
            (List<Slot> newSlots, CancellationToken cancellationToken)
        {
            var serviceId = newSlots.Select(s => s.ServiceId).FirstOrDefault();
            var slots = await _uow.SlotRepository
                .GetAvailableSlotsByServiceIdAsync(serviceId, cancellationToken);
            var newSlotDates = newSlots.Select(x => x.SlotDateTime).ToList();

            var slotsToAdd = newSlots
                .Where(newSlot => !slots.Any(existing => existing.SlotDateTime == newSlot.SlotDateTime))
                .ToList();

            var slotsToDelete = slots
                .Where(existing => !newSlotDates.Contains(existing.SlotDateTime))
                .ToList();

            await _uow.SlotRepository.DeleteRange(slotsToDelete, cancellationToken);
            await _uow.SlotRepository.AddRange(slotsToAdd, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}