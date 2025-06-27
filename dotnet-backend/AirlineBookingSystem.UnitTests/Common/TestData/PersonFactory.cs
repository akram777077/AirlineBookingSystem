using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class PersonFactory
{
    public static Person Create(int id = 1)
    {
        return new Person
        {
            Id = id,
            FirstName = "Smil",
            MidName = "B.",
            LastName = "DarKozen",
            DOB = new DateTime(1999, 5, 2),
            PhoneNumber = "0550223456",
            Email = "example@example.com",
            ImagePath = null,
            Gender = 'M',
            AddressId = 1,
            Address = AddressFactory.Create()
        };
    }
}
