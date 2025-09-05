using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Users;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for users using AutoMapper.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfile"/> class.
    /// </summary>
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName.ToString()));
    }
}
