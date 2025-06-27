using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class AirportFactory
{
    public static Airport Create(int id = 1, string name = "Houari Boumediene", string code = "ALG")
    {
        return new Airport
        {
            Id = id,
            Name = name,
            AirportCode = code,
            CityId = 1,
            City = CityFactory.Create()
        };
    }
}
