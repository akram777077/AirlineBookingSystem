using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class FlightClassProfile : Profile
{
    public FlightClassProfile()
    {
        CreateMap<FlightClass, AirlineBookingSystem.Shared.DTOs.flightClasses.FlightClassDto>()
            .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.SeatCapacity))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.SeatCapacity - src.Seats.Count));
        CreateMap<CreateFlightClassDto, FlightClass>();
        CreateMap<UpdateFlightClassDto, FlightClass>();
        CreateMap<FlightClass, FlightClassDto>();
    }
}
