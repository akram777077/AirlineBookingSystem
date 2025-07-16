using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class CountryProfile:Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();
    }
}
