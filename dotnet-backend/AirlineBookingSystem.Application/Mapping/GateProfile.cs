using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class GateProfile : Profile
{
    public GateProfile()
    {
        CreateMap<Gate, GateDto>()
            .ForMember(dest => dest.TerminalName, opt => opt.MapFrom(src => src.Terminal.Name));
        CreateMap<CreateGateDto, Gate>();
        CreateMap<UpdateGateDto, Gate>();
    }
}
