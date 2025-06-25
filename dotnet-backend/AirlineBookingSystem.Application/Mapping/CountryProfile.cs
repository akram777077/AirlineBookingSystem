using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country,CountryDto>().ReverseMap();
    }
}