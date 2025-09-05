using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for countries using AutoMapper.
/// </summary>
public class CountryProfile:Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountryProfile"/> class.
    /// </summary>
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();
    }
}
