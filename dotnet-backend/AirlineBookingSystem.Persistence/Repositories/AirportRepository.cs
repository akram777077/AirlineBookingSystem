using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Airport entities.
/// </summary>
public class AirportRepository(ApplicationDbContext context)
    : GenericRepository<Airport>(context), IAirportRepository
{
    /// <summary>
    /// Retrieves an airport by its airport code.
    /// </summary>
    /// <param name="code">The airport code (e.g., "JFK").</param>
    /// <returns>A <see cref="Task{Airport}"/> representing the asynchronous operation. The task result contains the airport if found, otherwise null.</returns>
    public async Task<Airport?> GetByCodeAsync(string code)
    {
        return await Context.Airports
            .FirstOrDefaultAsync(a => a.AirportCode == code);
    }

    /// <summary>
    /// Retrieves a list of airports by their associated city ID.
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <returns>A <see cref="Task{List{Airport}}"/> representing the asynchronous operation. The task result contains a list of airports in the specified city.</returns>
    public async Task<List<Airport>> GetByCityIdAsync(int cityId)
    {
        return await Context.Airports
            .Include(a => a.City)
            .Where(a=>a.CityId == cityId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all airports as an IQueryable, including their associated city.
    /// </summary>
    /// <returns>An <see cref="IQueryable{Airport}"/> representing all airports with their city data.</returns>
    public IQueryable<Airport> GetAll()
    {
        return Context.Airports.Include(a => a.City).AsQueryable();
    }
}

