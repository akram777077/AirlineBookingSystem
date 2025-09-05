using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing gates.
/// </summary>
public interface IGateRepository : IGenericRepository<Gate>
{
    /// <summary>
    /// Gets all gates as a queryable collection.
    /// </summary>
    /// <returns>A queryable collection of all gates.</returns>
    IQueryable<Gate> GetAll();
}
