using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.flights;
using AutoMapper;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Mapping;

public class FlightProfile : Profile
{
    public FlightProfile()
    {

        CreateMap<Flight, FlightDetailsDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.FlightStatus.StatusName.ToString()))
            
            .ForMember(dest => dest.Airplane,
                opt => opt.MapFrom(src => new FlightDetailsDto.FlightAirplaneDto
                {
                    Model = src.Airplane.Model,
                    Manufacturer = src.Airplane.Manufacturer,
                    Code = src.Airplane.Code,
                    Capacity = src.Airplane.Capacity
                }))
            
            .ForMember(dest => dest.Departure,
                opt => opt.MapFrom(src => new FlightDetailsDto.FlightSegmentDto
                {
                    Gate = src.DepartureGate.GateNumber,
                    Terminal = src.DepartureGate.Terminal.Name,
                    Airport = new FlightDetailsDto.FlightAirportDto
                    {
                        Code = src.DepartureGate.Terminal.Airport.AirportCode,
                        Name = src.DepartureGate.Terminal.Airport.Name,
                        City = src.DepartureGate.Terminal.Airport.City.Name,
                        Country = src.DepartureGate.Terminal.Airport.City.Country.Name,
                        Timezone = src.DepartureGate.Terminal.Airport.Timezone
                    }
                }))

            .ForMember(dest => dest.Arrival,
                opt => opt.MapFrom(src => src.ArrivalGate != null ? new FlightDetailsDto.FlightSegmentDto
                {
                    Gate = src.ArrivalGate.GateNumber,
                    Terminal = src.ArrivalGate.Terminal.Name,
                    Airport = new FlightDetailsDto.FlightAirportDto
                    {
                        Code = src.ArrivalGate.Terminal.Airport.AirportCode,
                        Name = src.ArrivalGate.Terminal.Airport.Name,
                        City = src.ArrivalGate.Terminal.Airport.City.Name,
                        Country = src.ArrivalGate.Terminal.Airport.City.Country.Name,
                        Timezone = src.ArrivalGate.Terminal.Airport.Timezone
                    }
                } : null))

            .ForMember(dest => dest.TotalBookings,
                opt => opt.MapFrom(src => src.Bookings.Count))

            .ForMember(dest => dest.AvailableSeats,
                opt => opt.MapFrom(src => src.Airplane.Capacity - src.Bookings.Count))

            .ForMember(dest => dest.DepartureTime,
                opt => opt.MapFrom(src => src.DepartureTime))

            .ForMember(dest => dest.ArrivalTime,
                opt => opt.MapFrom(src => src.ArrivalTime));
    }
}