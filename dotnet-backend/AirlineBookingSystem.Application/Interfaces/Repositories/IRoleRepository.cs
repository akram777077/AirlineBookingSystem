using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing roles.
/// </summary>
public interface IRoleRepository : IGenericRepository<Role>
{
    /// <summary>
    /// Gets a read-only collection of all roles.
    /// </summary>
    /// <returns>A read-only collection of all roles.</returns>
    new Task<IReadOnlyCollection<Role>> GetAllAsync();

    /// <summary>
    /// Gets a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role.</param>
    /// <returns>The role, or null if not found.</returns>
    new Task<Role?> GetByIdAsync(int id);
}
