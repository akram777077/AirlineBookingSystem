using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing genders.
/// </summary>
public interface IGenderRepository : IGenericRepository<Gender>
{
    /// <summary>
    /// Gets a gender by its ID.
    /// </summary>
    /// <param name="id">The ID of the gender.</param>
    /// <returns>The gender, or null if not found.</returns>
    new Task<Gender?> GetByIdAsync(int id);

    /// <summary>
    /// Gets a read-only collection of all genders.
    /// </summary>
    /// <returns>A read-only collection of all genders.</returns>
    new Task<IReadOnlyCollection<Gender>> GetAllAsync();
}
