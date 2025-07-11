using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class SeatFactory
{
    public static Faker<Seat> GetSeatFaker(int flightClassId)
    {
        return new Faker<Seat>()
            .RuleFor(s => s.SeatNumber, f => f.Finance.Account(2))
            .RuleFor(s => s.FlightClassId, flightClassId);
    }
}