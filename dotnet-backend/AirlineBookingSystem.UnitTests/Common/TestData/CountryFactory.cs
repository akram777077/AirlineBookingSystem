using AirlineBookingSystem.Domain.Entities;

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
}
