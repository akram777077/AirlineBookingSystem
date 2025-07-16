using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class BookingStatusProfile : Profile
{
    public BookingStatusProfile()
    {
        CreateMap<BookingStatus, BookingStatusDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BookingStatusName.ToString()));
    }
}
