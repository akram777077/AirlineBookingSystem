using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Domain.Entities;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<Seat, SeatDto>();
    }
}