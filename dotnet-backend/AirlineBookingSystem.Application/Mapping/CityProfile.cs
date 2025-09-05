using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for cities using AutoMapper.
/// </summary>
public class CityProfile:Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CityProfile"/> class.
    /// </summary>
    public CityProfile()
    {
        CreateMap<City, CityDto>();
    }
}
