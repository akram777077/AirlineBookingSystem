using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing class types.
/// </summary>
public interface IClassTypeRepository : IGenericRepository<ClassType>
{
    /// <summary>
    /// Gets a class type by its ID.
    /// </summary>
    /// <param name="id">The ID of the class type.</param>
    /// <returns>The class type, or null if not found.</returns>
    new Task<ClassType?> GetByIdAsync(int id);

    /// <summary>
    /// Gets a read-only collection of all class types.
    /// </summary>
    /// <returns>A read-only collection of all class types.</returns>
    new Task<IReadOnlyCollection<ClassType>> GetAllAsync();
}
