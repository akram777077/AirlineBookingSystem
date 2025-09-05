using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing cities.
/// </summary>
public interface ICityRepository : IGenericRepository<City>
{
    /// <summary>
    /// Gets all cities as a queryable collection.
    /// </summary>
    /// <returns>A queryable collection of all cities.</returns>
    IQueryable<City> GetAll();

    /// <summary>
    /// Gets a read-only collection of cities by country ID.
    /// </summary>
    /// <param name="countryId">The ID of the country.</param>
    /// <returns>A read-only collection of cities in the specified country.</returns>
    Task<IReadOnlyCollection<City>> GetByCountryIdAsync(int countryId);
}