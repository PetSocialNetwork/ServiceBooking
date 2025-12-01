using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Services;
using ServiceBooking.WebApi.Models.Requests;
using ServiceBooking.WebApi.Models.Responses;

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
        /// Добавляет свобдные слоьы, удаляет ненужные 
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task UpdateSlotsAsync
            ([FromBody] List<AddSlotRequest> request, CancellationToken cancellationToken)
        {
            var slots = _mapper.Map<List<Slot>>(request);
            await _bookingService.UpdateSlotsAsync(slots, cancellationToken);     
        }
    }
}
