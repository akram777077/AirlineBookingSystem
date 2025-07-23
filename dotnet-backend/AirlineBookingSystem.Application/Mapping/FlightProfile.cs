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
                opt => opt.MapFrom(src => new FlightAirplaneDto(
                    src.Airplane.Model,
                    src.Airplane.Manufacturer,
                    src.Airplane.Code,
                    src.Airplane.Capacity
                )))
            
            .ForMember(dest => dest.Departure,
                opt => opt.MapFrom(src => new FlightSegmentDto(
                    src.DepartureGate.GateNumber,
                    src.DepartureGate.Terminal.Name,
                    new FlightAirportDto(
                        src.DepartureGate.Terminal.Airport.AirportCode,
                        src.DepartureGate.Terminal.Airport.Name,
                        src.DepartureGate.Terminal.Airport.City.Name,
                        src.DepartureGate.Terminal.Airport.City.Country.Name,
                        src.DepartureGate.Terminal.Airport.Timezone
                    )
                )))

            .ForMember(dest => dest.Arrival,
                opt => opt.MapFrom(src => src.ArrivalGate != null ? new FlightSegmentDto(
                src.ArrivalGate.GateNumber,
                src.ArrivalGate.Terminal.Name,
                new FlightAirportDto(
                    src.ArrivalGate.Terminal.Airport.AirportCode,
                    src.ArrivalGate.Terminal.Airport.Name,
                    src.ArrivalGate.Terminal.Airport.City.Name,
                    src.ArrivalGate.Terminal.Airport.City.Country.Name,
                    src.ArrivalGate.Terminal.Airport.Timezone
                )
            ) : null))

            .ForMember(dest => dest.TotalBookings,
                opt => opt.MapFrom(src => src.Bookings.Count))

            .ForMember(dest => dest.AvailableSeats,
                opt => opt.MapFrom(src => src.Airplane.Capacity - src.Bookings.Count))

            .ForMember(dest => dest.DepartureTime,
                opt => opt.MapFrom(src => src.DepartureTime))

            .ForMember(dest => dest.ArrivalTime,
                opt => opt.MapFrom(src => src.ArrivalTime));

        

        CreateMap<Flight, FlightSearchResultDto>()
            .ForMember(dest => dest.Airline,
                opt => opt.MapFrom(src => src.Airplane.Manufacturer + " " + src.Airplane.Model))
            .ForMember(dest => dest.Departure, opt => opt.MapFrom(src => new FlightSegmentSearchDto(
                src.DepartureGate.Terminal.Airport.City.Name,
                src.DepartureGate.Terminal.Airport.City.Country.Code,
                src.DepartureGate.Terminal.Airport.AirportCode,
                src.DepartureTime
            )))
            .ForMember(dest => dest.Arrival, opt => opt.MapFrom(src => new FlightSegmentSearchDto(
                src.ArrivalGate!.Terminal.Airport.City.Name,
                src.ArrivalGate.Terminal.Airport.City.Country.Code,
                src.ArrivalGate.Terminal.Airport.AirportCode,
                src.ArrivalTime
            )))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.FlightStatus.StatusName.ToString()));

        CreateMap<CreateFlightDto, Flight>();
        CreateMap<UpdateFlightDto, Flight>();
    }
}