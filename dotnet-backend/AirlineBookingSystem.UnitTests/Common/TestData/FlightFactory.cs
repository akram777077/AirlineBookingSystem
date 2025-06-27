using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class FlightFactory
{
    public static Flight Create(int id = 1, string number = "AH1010")
    {
        return new Flight
        {
            Id = id,
            FlightNumber = number,
            DepartureTime = DateTime.UtcNow.AddHours(2),
            ArrivalTime = DateTime.UtcNow.AddHours(5),
            FromAirportId = 1,
            ToAirportId = 2,
            FromAirport = AirportFactory.Create(1),
            ToAirport = AirportFactory.Create(2)
        };
    }
}
