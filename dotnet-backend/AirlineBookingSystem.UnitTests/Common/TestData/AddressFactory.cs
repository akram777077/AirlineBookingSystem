using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class AddressFactory
{
    public static Address Create(int id = 1, string street = "123 Main St", string zip = "16000")
    {
        return new Address
        {
            Id = id,
            Street = street,
            ZipCode = zip,
            CityId = 1,
            City = CityFactory.Create()
        };
    }
    
    public static AddressDto ToDto(this Address address)
    {
        return new AddressDto
        {
            Id = address.Id,
            Street = address.Street,
            ZipCode = address.ZipCode,
            CityId = address.CityId,
            CountryId = address.City.CountryId
        };
    }
}
