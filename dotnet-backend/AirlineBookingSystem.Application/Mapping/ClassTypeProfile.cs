using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for class types using AutoMapper.
/// </summary>
public class ClassTypeProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassTypeProfile"/> class.
    /// </summary>
    public ClassTypeProfile()
    {
        CreateMap<ClassType, ClassTypeDto>();
    }
}
