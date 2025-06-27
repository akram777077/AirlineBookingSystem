using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class CityFactory
{
    public static City Create(int id = 1, string name = "Algiers")
    {
        return new City
        {
            Id = id,
            Name = name,
            CountryId = 1,
            Country = CountryFactory.Create()
        };
    }
}
