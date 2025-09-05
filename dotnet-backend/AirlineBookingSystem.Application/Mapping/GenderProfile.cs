using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for genders using AutoMapper.
/// </summary>
public class GenderProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenderProfile"/> class.
    /// </summary>
    public GenderProfile()
    {
        CreateMap<Gender, GenderDto>();
    }
}
