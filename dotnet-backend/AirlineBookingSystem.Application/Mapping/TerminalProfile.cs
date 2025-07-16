using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class TerminalProfile : Profile
{
    public TerminalProfile()
    {
        CreateMap<Terminal, TerminalDto>();
        CreateMap<CreateTerminalDto, Terminal>();
        CreateMap<UpdateTerminalDto, Terminal>();
    }
}
