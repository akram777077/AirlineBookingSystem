using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;

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

    public static CityDto ToDto(this City city)
    {
        return new CityDto
        {
            Id = city.Id,
            Name = city.Name,
            CountryId = city.CountryId
        };
    }
}
