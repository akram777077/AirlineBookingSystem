using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for gates using AutoMapper.
/// </summary>
public class GateProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GateProfile"/> class.
    /// </summary>
    public GateProfile()
    {
        CreateMap<Gate, GateDto>()
            .ForMember(dest => dest.TerminalName, opt => opt.MapFrom(src => src.Terminal.Name));
        CreateMap<CreateGateDto, Gate>();
        CreateMap<UpdateGateDto, Gate>();
    }
}
