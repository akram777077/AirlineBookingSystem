using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class CountryFactory
{
    public static Faker<Country> GetCountryFaker()
    {
        return new Faker<Country>()
            .RuleFor(c => c.Name, f => f.Address.Country())
            .RuleFor(c => c.Code, f => f.Address.CountryCode());
    }
}