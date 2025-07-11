using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class TerminalFactory
{
    public static Faker<Terminal> GetTerminalFaker(int airportId)
    {
        return new Faker<Terminal>()
            .RuleFor(t => t.Name, f => f.Address.StreetName() + " Terminal")
            .RuleFor(t => t.AirportId, airportId);
    }
}