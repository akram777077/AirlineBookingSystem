using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<Airport, AirportDto>()
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.City.Id))
            .ReverseMap();
    }
}