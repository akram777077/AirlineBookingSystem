using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<Domain.Entities.City, Shared.DTOs.Countries.CityDto>()
            .ReverseMap();
    }
}