using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<Domain.Entities.City, CityDto>()
            .ReverseMap();
    }
}