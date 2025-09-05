using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for booking statuses using AutoMapper.
/// </summary>
public class BookingStatusProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookingStatusProfile"/> class.
    /// </summary>
    public BookingStatusProfile()
    {
        CreateMap<BookingStatus, BookingStatusDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BookingStatusName.ToString()));
    }
}
