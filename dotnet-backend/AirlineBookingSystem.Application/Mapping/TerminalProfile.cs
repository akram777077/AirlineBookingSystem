using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for terminals using AutoMapper.
/// </summary>
public class TerminalProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TerminalProfile"/> class.
    /// </summary>
    public TerminalProfile()
    {
        CreateMap<Terminal, TerminalDto>();
        CreateMap<CreateTerminalDto, Terminal>();
        CreateMap<UpdateTerminalDto, Terminal>();
    }
}
