using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<Airport, AirportDto>().ReverseMap();
        CreateMap<Airport, AirportSearchResultDto>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        CreateMap<UpdateAirportDto, Airport>();
    }
}