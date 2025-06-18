using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>()
            .ReverseMap();
        
        CreateMap<AddressDto, Address>()
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore());
    }
}