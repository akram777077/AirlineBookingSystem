using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class PersonFactory
{
    public static Faker<AirlineBookingSystem.Domain.Entities.Person> GetPersonFaker(int addressId, int genderId)
    {
        return new Faker<AirlineBookingSystem.Domain.Entities.Person>()
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth)
            .RuleFor(p => p.Email, f => f.Person.Email)
            .RuleFor(p => p.PhoneNumber, f => f.Person.Phone)
            .RuleFor(p => p.AddressId, addressId)
            .RuleFor(p => p.GenderId, genderId);
    }
}