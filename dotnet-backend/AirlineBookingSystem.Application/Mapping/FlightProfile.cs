using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>()
            .ForMember(dst => dst.FromAirportCode,
                opt => opt.MapFrom(src => src.FromAirport.AirportCode))
            .ForMember(dst => dst.ToAirportCode,
                opt => opt.MapFrom(src => src.ToAirport.AirportCode))
            .ForMember(dst => dst.FromCity,
                opt => opt.MapFrom(src => src.FromAirport.City.Name))
            .ForMember(dst => dst.ToCity,
                opt => opt.MapFrom(src => src.ToAirport.City.Name));
    }
}