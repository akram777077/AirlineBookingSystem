using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class UserFactory
{
    public static Faker<User> GetUserFaker(int personId, int roleId)
    {
        return new Faker<User>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.PersonId, personId)
            .RuleFor(u => u.RoleId, roleId);
    }
}