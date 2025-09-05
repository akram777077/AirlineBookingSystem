
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for airplanes using AutoMapper.
/// </summary>
public class AirplaneProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AirplaneProfile"/> class.
    /// </summary>
    public AirplaneProfile()
    {
        CreateMap<Airplane, AirplaneDto>().ReverseMap();
        CreateMap<CreateAirplaneDto, Airplane>();
        CreateMap<UpdateAirplaneDto, Airplane>();
    }
}
