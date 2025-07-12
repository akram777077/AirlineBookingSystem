using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class CityFactory
{
    public static Faker<City> GetCityFaker(int countryId)
    {
        return new Faker<City>()
            .RuleFor(c => c.Name, f => f.Address.City())
            .RuleFor(c => c.CountryId, countryId);
    }
}