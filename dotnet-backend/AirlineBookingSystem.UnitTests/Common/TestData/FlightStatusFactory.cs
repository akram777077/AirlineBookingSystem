using AirlineBookingSystem.Domain.Entities;
using Bogus;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class FlightStatusFactory
{
    public static Faker<FlightStatus> GetFlightStatusFaker()
    {
        return new Faker<FlightStatus>()
            .RuleFor(fs => fs.StatusName, f => f.PickRandom<FlightStatusEnum>());
    }
}