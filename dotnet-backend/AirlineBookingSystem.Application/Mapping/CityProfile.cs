using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class CityProfile:Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
    }
}
