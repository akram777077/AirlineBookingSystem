using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Enums;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role,RoleDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => Enum.Parse<UserRoleEnum>(src.RoleName)));
    }
}