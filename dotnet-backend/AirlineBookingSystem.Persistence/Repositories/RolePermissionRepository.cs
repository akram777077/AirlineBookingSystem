using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class RolePermissionRepository(ApplicationDbContext context) : GenericRepository<RolePermission>(context), IRolePermissionRepository
{
    public async Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId)
    {
        return await context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(int roleId, int permissionId)
    {
        return await context.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
    }
}
