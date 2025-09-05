using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing airports.
/// </summary>
public interface IAirportRepository : IGenericRepository<Airport>
{
    /// <summary>
    /// Gets an airport by its code.
    /// </summary>
    /// <param name="code">The code of the airport.</param>
    /// <returns>The airport, or null if not found.</returns>
    Task<Airport?> GetByCodeAsync(string code);

    /// <summary>
    /// Gets a list of airports by city ID.
    /// </summary>
    /// <param name="cityId">The ID of the city.</param>
    /// <returns>A list of airports in the specified city.</returns>
    Task<List<Airport>> GetByCityIdAsync(int cityId);

    /// <summary>
    /// Gets all airports as a queryable collection.
    /// </summary>
    /// <returns>A queryable collection of all airports.</returns>
    IQueryable<Airport> GetAll();
}