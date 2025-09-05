using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Domain.Entities;
using AutoMapper;

namespace AirlineBookingSystem.Application.Mapping;

/// <summary>
/// Configures mapping profiles for seats using AutoMapper.
/// </summary>
public class SeatProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeatProfile"/> class.
    /// </summary>
    public SeatProfile()
    {
        CreateMap<Seat, SeatDto>();
    }
}