using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for roles using AutoMapper.
/// </summary>
public class RoleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleProfile"/> class.
    /// </summary>
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName.ToString()));
    }
}
