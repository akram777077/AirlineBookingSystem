using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AirlineBookingSystem.Shared.Enums;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class BookingStatusProfile : Profile
{
    public BookingStatusProfile()
    {
        CreateMap<BookingStatus, BookingStatusDto>()
            .ForMember(dest => dest.StatusName,
                opt
                    => opt.MapFrom(src => src.StatusName.ToString()));
        CreateMap<BookingStatusDto, BookingStatus>()
            .ForMember(dest => dest.StatusName,
                opt 
                    => opt.MapFrom(src => Enum.Parse<BookingStatusEnum>(src.StatusName)));
    }
}