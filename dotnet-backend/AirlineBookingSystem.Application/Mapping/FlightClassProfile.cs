using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class FlightClassProfile : Profile
{
    public FlightClassProfile()
    {
        CreateMap<FlightClass, FlightClassDto>()
            .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.SeatCapacity))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.SeatCapacity - src.Seats.Count));
    }
}
