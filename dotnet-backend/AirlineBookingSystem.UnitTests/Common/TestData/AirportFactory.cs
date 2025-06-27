using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;

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
    public static AirportDto ToDto(this Airport airport)
    {
        return new AirportDto
        {
            Id = airport.Id,
            Name = airport.Name,
            AirportCode = airport.AirportCode,
            CityId = airport.CityId
        };
    }
}
