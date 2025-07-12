using AirlineBookingSystem.Domain.Entities;
using Bogus;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class RoleFactory
{
    public static Faker<Role> GetRoleFaker()
    {
        return new Faker<Role>()
            .RuleFor(r => r.RoleName, f => f.PickRandom<RoleEnum>());
    }
}