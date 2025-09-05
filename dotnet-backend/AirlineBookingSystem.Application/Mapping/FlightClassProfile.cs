using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for flight classes using AutoMapper.
/// </summary>
public class FlightClassProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FlightClassProfile"/> class.
    /// </summary>
    public FlightClassProfile()
    {
        CreateMap<FlightClass, AirlineBookingSystem.Shared.DTOs.flightClasses.FlightClassDto>()
            .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.SeatCapacity))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.SeatCapacity));
        CreateMap<CreateFlightClassDto, FlightClass>();
        CreateMap<UpdateFlightClassDto, FlightClass>();
        CreateMap<FlightClass, FlightClassDto>();
    }
}
