using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class UserAirportFactory
{
    public static Faker<UserAirport> GetUserAirportFaker(int userId, int airportId)
    {
        return new Faker<UserAirport>()
            .RuleFor(ua => ua.UserId, userId)
            .RuleFor(ua => ua.AirportId, airportId);
    }
}