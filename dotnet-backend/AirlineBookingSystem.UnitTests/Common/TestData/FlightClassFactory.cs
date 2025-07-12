using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class FlightClassFactory
{
    public static Faker<FlightClass> GetFlightClassFaker(int flightId, int classTypeId)
    {
        return new Faker<FlightClass>()
            .RuleFor(fc => fc.Price, f => f.Random.Decimal(100, 1000))
            .RuleFor(fc => fc.SeatCapacity, f => f.Random.Int(10, 100))
            .RuleFor(fc => fc.FlightId, flightId)
            .RuleFor(fc => fc.ClassTypeId, classTypeId);
    }
}