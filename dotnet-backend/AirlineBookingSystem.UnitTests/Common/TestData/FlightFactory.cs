using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class FlightFactory
{
    public static Flight Create(int id = 1, string number = "AH1010",DateTime time = default)
    {
        return new Flight
        {
            Id = id,
            FlightNumber = number,
            DepartureTime = time == default ? DateTime.UtcNow.AddHours(2) : time.AddHours(2),
            ArrivalTime = time == default? DateTime.UtcNow.AddHours(5) : time.AddHours(5),
            FromAirportId = 1,
            ToAirportId = 2,
            FromAirport = AirportFactory.Create(1),
            ToAirport = AirportFactory.Create(2)
        };
    }
    public static FlightDto ToDto(this Flight flight)
    {
        return new FlightDto
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            FromAirportCode = flight.FromAirport.AirportCode,
            ToAirportCode = flight.ToAirport.AirportCode,
            FromCity = flight.FromAirport.City.Name,
            ToCity = flight.ToAirport.City.Name
        };
    }
}
