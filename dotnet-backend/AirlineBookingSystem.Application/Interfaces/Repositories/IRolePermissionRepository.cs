using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing role permissions.
/// </summary>
public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
    /// <summary>
    /// Gets a read-only list of permissions for a given role ID.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <returns>A read-only list of permissions.</returns>
    Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId);

    /// <summary>
    /// Gets a role permission by role ID and permission ID.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="permissionId">The ID of the permission.</param>
    /// <returns>The role permission, or null if not found.</returns>
    Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(int roleId, int permissionId);
}

