using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for permissions using AutoMapper.
/// </summary>
public class PermissionProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionProfile"/> class.
    /// </summary>
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()));
    }
}
