using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class AirplaneFactory
{
    public static Faker<Airplane> GetAirplaneFaker()
    {
        return new Faker<Airplane>()
            .RuleFor(a => a.Model, f => f.Vehicle.Model())
            .RuleFor(a => a.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(a => a.Capacity, f => f.Random.Int(50, 500))
            .RuleFor(a => a.Code, f => f.Finance.Account(3));
    }
}