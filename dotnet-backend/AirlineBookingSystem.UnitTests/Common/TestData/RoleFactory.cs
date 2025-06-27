using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class RoleFactory
{
    public static Role Create(int id = 1, UserRoleEnum role = UserRoleEnum.Admin)
    {
        return new Role
        {
            Id = id,
            RoleName = role
        };
    }
    
    public static RoleDto ToDto(this Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            RoleName = role.RoleName.ToString()
        };
    }
}
