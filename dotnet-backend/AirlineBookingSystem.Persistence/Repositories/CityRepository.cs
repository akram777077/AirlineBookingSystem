using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing City entities.
/// </summary>
public class CityRepository(ApplicationDbContext context)
    : GenericRepository<City>(context), ICityRepository
{
    /// <summary>
    /// Retrieves all cities as an IQueryable.
    /// </summary>
    /// <returns>An <see cref="IQueryable{City}"/> representing all cities.</returns>
    public IQueryable<City> GetAll() => Context.Cities.AsQueryable();

    /// <summary>
    /// Retrieves a list of cities by their associated country ID.
    /// </summary>
    /// <param name="countryId">The unique identifier of the country.</param>
    /// <returns>A <see cref="Task{IReadOnlyCollection{City}}"/> representing the asynchronous operation. The task result contains a read-only collection of cities in the specified country.</returns>
    public async Task<IReadOnlyCollection<City>> GetByCountryIdAsync(int countryId)
    {
        return await Context.Cities
            .Where(city => city.CountryId == countryId)
            .ToListAsync();
    }
}

