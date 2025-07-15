
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class AirplaneProfile : Profile
{
    public AirplaneProfile()
    {
        CreateMap<Airplane, AirplaneDto>().ReverseMap();
        CreateMap<CreateAirplaneDto, Airplane>();
        CreateMap<UpdateAirplaneDto, Airplane>();
    }
}
