using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class ClassTypeProfile : Profile
{
    public ClassTypeProfile()
    {
        CreateMap<ClassType, ClassTypeDto>();
    }
}
