using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class UserFactory
{
    public static User Create(int id = 1)
    {
        return new User
        {
            Id = id,
            Username = "admin",
            Password = "admin",
            CreatedAt = DateTime.UtcNow,
            LastLogin = null,
            IsActive = true,
            PersonId = 1,
            Person = PersonFactory.Create(),
            RoleId = 1,
            Role = RoleFactory.Create()
        };
    }
}
