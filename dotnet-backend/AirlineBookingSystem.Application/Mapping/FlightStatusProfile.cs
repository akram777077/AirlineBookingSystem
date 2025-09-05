using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for flight statuses using AutoMapper.
/// </summary>
public class FlightStatusProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FlightStatusProfile"/> class.
    /// </summary>
    public FlightStatusProfile()
    {
        CreateMap<FlightStatus, FlightStatusDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StatusName.ToString()));
    }
}
