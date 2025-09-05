using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing permissions.
/// </summary>
public interface IPermissionRepository : IGenericRepository<Permission>
{
    /// <summary>
    /// Gets a read-only collection of all permissions.
    /// </summary>
    /// <returns>A read-only collection of all permissions.</returns>
    new Task<IReadOnlyCollection<Permission>> GetAllAsync();

    /// <summary>
    /// Gets a permission by its ID.
    /// </summary>
    /// <param name="id">The ID of the permission.</param>
    /// <returns>The permission, or null if not found.</returns>
    new Task<Permission?> GetByIdAsync(int id);
}

