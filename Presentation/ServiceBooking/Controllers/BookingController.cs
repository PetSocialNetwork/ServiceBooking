using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Services;
using ServiceBooking.WebApi.Models.Requests;
using ServiceBooking.WebApi.Models.Responses;
using static System.Reflection.Metadata.BlobBuilder;

namespace ServiceBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingController(BookingService bookingService,
            IMapper mapper)
        {
            _bookingService = bookingService
                ?? throw new ArgumentException(nameof(bookingService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Возвращает все свободные даты по идентификатору услуги
        /// </summary>
        /// <param name="serviceId">Идентификатор услуги</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<SlotReponse>> GetAvailableSlotsAsync
            ([FromQuery] Guid serviceId, CancellationToken cancellationToken)
        {
            var slots = await _bookingService
                .GetAvailableSlotsByServiceIdAsync(serviceId, cancellationToken);
            return _mapper.Map<List<SlotReponse>>(slots);
        }

        /// <summary>
        /// Проверяет есть ли занятые даты по идентификатору услуги
        /// </summary>
        /// <param name="serviceId">Идентификатор услуги</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<bool> IsBusySlotsExistsAsync
            ([FromQuery] Guid serviceId, CancellationToken cancellationToken)
        {
            return await _bookingService
                .IsBusySlotsExistsAsync(serviceId, cancellationToken);
        }






        /// <summary>
        /// Возвращает все бронирования по идентификатору услуги
        /// </summary>
        /// <param name="serviceId">Идентификатор услуги</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<BookingResponse>> GetBookingsByServiceIdAsync
            ([FromQuery] Guid serviceId, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService
                .GetBookingsByServiceIdAsync(serviceId, cancellationToken);
            return _mapper.Map<List<BookingResponse>>(bookings);
        }

        /// <summary>
        /// Возвращает все бронирования по профилю пользователя
        /// </summary>
        /// <param name="profileId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<BookingResponse>> GetBookingsByProfileIdAsync
            ([FromQuery] Guid profileId, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService
                .GetBookingsByProfileIdAsync(profileId, cancellationToken);
            return _mapper.Map<List<BookingResponse>>(bookings);
        }

        /// <summary>
        /// Добавляет бронирование
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task AddBookingAsync
            ([FromBody] AddBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);
            await _bookingService.AddBookingAsync(booking, cancellationToken);
        }








        /// <summary>
        /// Обновляет статус бронирования
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task UpdateBookingStatusAsync
            ([FromBody] UpdateBookingStatusRequest request, CancellationToken cancellationToken)
        {
            await _bookingService.UpdateBookingStatusAsync
                (request.BookingId, request.Status, cancellationToken);
        }

        /// <summary>
        /// Добавляет свобдные слоты, удаляет ненужные 
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task UpdateSlotsAsync(
            [FromQuery] Guid serviceId,
            [FromBody] List<AddSlotRequest> request,
            CancellationToken cancellationToken)
        {
            if (request.Count == 0)
            {
                await _bookingService.DeleteAllSlotsForServiceAsync(serviceId, cancellationToken);
                return;
            }

            var slots = _mapper.Map<List<Slot>>(request)
            .Select(slot =>
            {
                slot.ServiceId = serviceId;
                return slot;
            })
            .ToList();
            await _bookingService.UpdateSlotsAsync(serviceId, slots, cancellationToken);
        }
    }
}
