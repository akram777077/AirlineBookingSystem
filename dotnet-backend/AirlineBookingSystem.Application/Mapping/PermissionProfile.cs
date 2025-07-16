using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()));
    }
}
