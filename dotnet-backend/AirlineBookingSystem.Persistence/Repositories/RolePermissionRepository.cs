using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing RolePermission entities.
/// </summary>
public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Retrieves all permissions associated with a specific role ID.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <returns>A <see cref="Task{IReadOnlyList{Permission}}"/> representing the asynchronous operation. The task result contains a read-only list of permissions for the specified role.</returns>
    public async Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId)
    {
        return await Context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific role-permission entry by role ID and permission ID.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="permissionId">The unique identifier of the permission.</param>
    /// <returns>A <see cref="Task{RolePermission}"/> representing the asynchronous operation. The task result contains the role-permission entry if found, otherwise null.</returns>
    public async Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(int roleId, int permissionId)
    {
        return await Context.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
    }
}