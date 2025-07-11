using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class RolePermissionFactory
{
    public static Faker<RolePermission> GetRolePermissionFaker(int roleId, int permissionId)
    {
        return new Faker<RolePermission>()
            .RuleFor(rp => rp.RoleId, roleId)
            .RuleFor(rp => rp.PermissionId, permissionId);
    }
}