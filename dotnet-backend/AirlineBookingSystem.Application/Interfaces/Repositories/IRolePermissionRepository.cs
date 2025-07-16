using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
    Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId);
    Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(int roleId, int permissionId);
}

