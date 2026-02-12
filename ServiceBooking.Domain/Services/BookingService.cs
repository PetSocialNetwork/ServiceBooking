using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Entities.Enums;
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

        public async Task<bool> IsBusySlotsExistsAsync
            (Guid serviceId, CancellationToken cancellationToken)
        {
            return await _uow.SlotRepository
                .IsBusySlotsExistsAsync(serviceId, cancellationToken);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByServiceIdAsync
          (Guid serviceId, CancellationToken cancellationToken)
        {
            return await _uow.BookingRepository
                .GetBookingsByServiceId(serviceId, cancellationToken);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByProfileIdAsync
         (Guid profileId, CancellationToken cancellationToken)
        {
            return await _uow.BookingRepository
                .GetBookingsByProfileId(profileId, cancellationToken);
        }

        public async Task UpdateBookingStatusAsync
          (Guid bookingId, BookingStatus status, CancellationToken cancellationToken)
        {
            var booking = await _uow.BookingRepository
                .GetById(bookingId, cancellationToken);
            booking.Status = status;

            var slot = await _uow.SlotRepository
                .GetById(booking.SlotId, cancellationToken);

            if (status == BookingStatus.Cancelled)
            {
                if (slot.SlotDateTime >= DateTime.UtcNow)
                {
                    slot.IsAvailable = true;
                    await _uow.SaveChangesAsync(cancellationToken);
                }
            }

            await _uow.BookingRepository.Update(booking, cancellationToken);
        }

        public async Task DeleteBookingAsync
         (Guid bookingId, CancellationToken cancellationToken)
        {
            var booking = await _uow.BookingRepository
                .FindBookingById(bookingId, cancellationToken)
                ?? throw new BookingNotFoundException("Бронирование не найдено.");

            if (booking.Status == BookingStatus.Confirmed)
            {
                throw new BookingInvalidDeleteException
                    ($"Невозможно удалить бронирование со статусом {booking.Status}");
            }

            var slot = await _uow.SlotRepository
                .GetById(booking.SlotId, cancellationToken);

            if (slot.SlotDateTime >= DateTime.UtcNow)
            {
                slot.IsAvailable = true;
                await _uow.BookingRepository.Delete(booking, cancellationToken);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            else
            {
                await _uow.BookingRepository.Delete(booking, cancellationToken);
                await _uow.SlotRepository.Delete(slot, cancellationToken);
            }
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

        public async Task DeleteAllSlotsForServiceAsync(Guid serviceId, CancellationToken cancellationToken)
        {
            await _uow.SlotRepository.DeleteAllStoltsByServiceIdAsync(serviceId, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSlotsAsync
            (Guid serviceId, List<Slot> newSlots, CancellationToken cancellationToken)
        {
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