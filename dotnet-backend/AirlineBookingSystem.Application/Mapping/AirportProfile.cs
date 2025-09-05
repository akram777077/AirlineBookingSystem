using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for airports using AutoMapper.
/// </summary>
public class AirportProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AirportProfile"/> class.
    /// </summary>
    public AirportProfile()
    {
        CreateMap<Airport, AirportDto>().ReverseMap();
        CreateMap<Airport, AirportSearchResultDto>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        CreateMap<UpdateAirportDto, Airport>();
    }
}