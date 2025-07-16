using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class GenderProfile : Profile
{
    public GenderProfile()
    {
        CreateMap<Gender, GenderDto>();
    }
}
