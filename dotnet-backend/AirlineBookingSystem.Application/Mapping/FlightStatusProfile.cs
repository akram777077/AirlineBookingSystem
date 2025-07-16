using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class FlightStatusProfile : Profile
{
    public FlightStatusProfile()
    {
        CreateMap<FlightStatus, FlightStatusDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StatusName.ToString()));
    }
}
