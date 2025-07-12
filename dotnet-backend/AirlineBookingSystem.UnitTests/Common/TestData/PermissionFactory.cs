using AirlineBookingSystem.Domain.Entities;
using Bogus;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class PermissionFactory
{
    public static Faker<Permission> GetPermissionFaker()
    {
        return new Faker<Permission>()
            .RuleFor(p => p.Name, f => f.PickRandom<PermissionEnum>());
    }
}