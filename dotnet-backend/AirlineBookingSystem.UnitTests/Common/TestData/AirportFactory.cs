using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class AirportFactory
{
    public static Faker<Airport> GetAirportFaker(int cityId)
    {
        return new Faker<Airport>()
            .RuleFor(a => a.AirportCode, f => f.Finance.Account(3).ToUpper())
            .RuleFor(a => a.Name, f => f.Company.CompanyName() + " Airport")
            .RuleFor(a => a.Timezone, f => f.Address.StateAbbr())
            .RuleFor(a => a.CityId, cityId);
    }
}