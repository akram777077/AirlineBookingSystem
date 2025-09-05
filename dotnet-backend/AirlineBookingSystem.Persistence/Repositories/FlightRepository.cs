using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using AirlineBookingSystem.Shared.Filters;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Flight entities, including detailed retrieval and soft deletion.
/// </summary>
public class FlightRepository(ApplicationDbContext context)
    : GenericRepository<Flight>(context), IFlightRepository
{
    /// <summary>
    /// Retrieves a flight by its ID, including all related details (airplane, departure/arrival gates, terminals, airports, cities, countries, and flight status).
    /// Soft-deleted flights are excluded from the result.
    /// </summary>
    /// <param name="id">The unique identifier of the flight.</param>
    /// <returns>A <see cref="Task{Flight}"/> representing the asynchronous operation. The task result contains the flight if found, otherwise null.</returns>
    public override Task<Flight?> GetByIdAsync(int id)
    {
        return Context.Flights
            .Where(f => f.DeletedAt == null)
            .Include(f => f.Airplane)
            .Include(f => f.DepartureGate)
            .ThenInclude(g => g.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Include(f => f.ArrivalGate)
            .ThenInclude(g => g!.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            
            .Include(f => f.FlightStatus)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    /// <summary>
    /// Retrieves a flight by its ID, optionally including its flight status.
    /// </summary>
    /// <param name="id">The unique identifier of the flight.</param>
    /// <param name="includeFlightStatus">A boolean indicating whether to include the flight status in the result.</param>
    /// <returns>A <see cref="Task{Flight}"/> representing the asynchronous operation. The task result contains the flight if found, otherwise null.</returns>
    public async Task<Flight?> GetByIdAsync(int id, bool includeFlightStatus)
    {
        if (includeFlightStatus)
        {
            return await Context.Flights.Include(f => f.FlightStatus).FirstOrDefaultAsync(f => f.Id == id);
        }
        return await Context.Flights.FirstOrDefaultAsync(f => f.Id == id);
    }

    /// <summary>
    /// Soft-deletes a flight by setting its <see cref="Flight.DeletedAt"/> timestamp.
    /// This method overrides the base <see cref="GenericRepository{T}.Delete"/> method to implement soft deletion.
    /// </summary>
    /// <param name="entity">The flight entity to soft-delete.</param>
    public override void Delete(Flight entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        Context.Flights.Update(entity);
    }

    /// <summary>
    /// Retrieves flights with detailed information based on a search filter.
    /// Includes related entities such as FlightStatus, Airplane, Departure/Arrival Gates, Terminals, Airports, Cities, and Countries.
    /// </summary>
    /// <param name="filter">The <see cref="FlightSearchFilter"/> containing search criteria (e.g., departure date, origin/destination city/country).</param>
    /// <returns>An <see cref="IQueryable{Flight}"/> representing the filtered flights with eager-loaded related entities.</returns>
    public IQueryable<Flight> GetFlightsWithDetails(FlightSearchFilter filter)
    {
        var query = Context.Flights
            .AsNoTracking()
            .Where(f => f.DeletedAt == null)
            .Include(f => f.FlightStatus)
            .Include(f => f.Airplane)
            .Include(f => f.DepartureGate)
            .ThenInclude(g => g.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Include(f => f.ArrivalGate)
            .ThenInclude(g => g!.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .AsQueryable();

        if (filter.DepartureDate.HasValue)
            query = query.Where(f => f.DepartureTime.Date == filter.DepartureDate.Value.Date);

        if (filter.FromCityId.HasValue)
            query = query.Where(f => f.DepartureGate != null && f.DepartureGate.Terminal != null && f.DepartureGate.Terminal.Airport != null && f.DepartureGate.Terminal.Airport.City != null && f.DepartureGate.Terminal.Airport.City.Id == filter.FromCityId);

        if (filter.ToCityId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal != null && f.ArrivalGate.Terminal.Airport != null && f.ArrivalGate.Terminal.Airport.City != null && f.ArrivalGate.Terminal.Airport.City.Id == filter.ToCityId);

        if (filter.FromCountryId.HasValue)
            query = query.Where(f => f.DepartureGate != null && f.DepartureGate.Terminal != null && f.DepartureGate.Terminal.Airport != null && f.DepartureGate.Terminal.Airport.City != null && f.DepartureGate.Terminal.Airport.City.Country != null && f.DepartureGate.Terminal.Airport.City.Country.Id == filter.FromCountryId);

        if (filter.ToCountryId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal != null && f.ArrivalGate.Terminal.Airport != null && f.ArrivalGate.Terminal.Airport.City != null && f.ArrivalGate.Terminal.Airport.City.Country != null && f.ArrivalGate.Terminal.Airport.City.Country.Id == filter.ToCountryId);

        return query;
    }

    /// <summary>
    /// Checks if a flight with the given flight number already exists in the system.
    /// </summary>
    /// <param name="flightNumber">The flight number to check for existence.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation. The task result is true if the flight number exists, false otherwise.</returns>
    public async Task<bool> IsFlightNumberExistsAsync(string flightNumber)
    {
        return await Context.Flights.AnyAsync(f => f.FlightNumber == flightNumber);
    }

}

