using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;

namespace AirlineBookingSystem.UnitTests.Common;

public static class TestData
{
    public static AddressDto GetAddressDto()
    {
        return new AddressDto
        {
            Id = 1,
            Street = "City Center",
            ZipCode = "27000",
            CityId = 2, 
            CountryId = 3, 
        };
    }
    public static List<AddressDto> GetAddressDtoList()
    {
        return new List<AddressDto>
        {
            new AddressDto() { Id = 1, Street = "City Center", ZipCode = "27000", CityId = 2, CountryId = 3 },
            new AddressDto() { Id = 2, Street = "Hello Street", ZipCode = "23400", CityId = 3, CountryId = 4 }
        };
    }
    public static List<Address> GetAddressesList()
    {
        return new List<Address>
        {
            new()
            {
                Id = 1, Street = "City Center", ZipCode = "27000",
                City = new City { Id = 2, Name = "Alger",
                    Country = new Country { Id = 3, Name = $"Algeria", Code = "Dz" }
                }
            },
            
            new()
            {
                Id = 2, Street = "Hello Street", ZipCode = "23400",
                City = new City { Id = 3, Name = "Oran",
                    Country = new Country { Id = 4, Name = $"Tunisia", Code = "TN" }
                }
            }
        };
    }
}