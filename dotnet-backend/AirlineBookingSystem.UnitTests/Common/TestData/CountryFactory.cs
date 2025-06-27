using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class CountryFactory
{
    public static Country Create(int id = 1, string name = "Algeria", string code = "DZ")
    {
        return new Country
        {
            Id = id,
            Name = name,
            Code = code
        };
    }

    public static CountryDto ToDto(this Country country)
    {
        return new CountryDto
        {
            Id = country.Id,
            Code = country.Code,
            Name = country.Name
        };
    }
}
