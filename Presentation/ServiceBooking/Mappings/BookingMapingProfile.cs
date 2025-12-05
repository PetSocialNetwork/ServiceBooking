using AutoMapper;
using ServiceBooking.Domain.Entities;
using ServiceBooking.Domain.Entities.Enums;
using ServiceBooking.WebApi.Models.Requests;
using ServiceBooking.WebApi.Models.Responses;

namespace ServiceBooking.WebApi.Mappings
{
    public class BookingMapingProfile : Profile
    {
        public BookingMapingProfile()
        {
            CreateMap<Slot, SlotReponse>();
            CreateMap<AddBookingRequest, Booking>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.SlotId, opt => opt.MapFrom(src => src.SlotId))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Pending))
                .ForMember(dest => dest.BookedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<AddSlotRequest, Slot>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.SlotDateTime, opt => opt.MapFrom(src => src.SlotDateTime))
               .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
               .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
