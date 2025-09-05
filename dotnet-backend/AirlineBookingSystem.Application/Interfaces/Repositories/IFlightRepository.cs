using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing flights.
/// </summary>
public interface IFlightRepository : IGenericRepository<Flight>
{
    /// <summary>
    /// Gets a flight by its ID, optionally including the flight status.
    /// </summary>
    /// <param name="id">The ID of the flight.</param>
    /// <param name="includeFlightStatus">A boolean indicating whether to include the flight status.</param>
    /// <returns>The flight, or null if not found.</returns>
    Task<Flight?> GetByIdAsync(int id, bool includeFlightStatus);

    /// <summary>
    /// Gets flights with details based on a search filter.
    /// </summary>
    /// <param name="filter">The flight search filter.</param>
    /// <returns>A queryable collection of flights with details.</returns>
    public IQueryable<Flight> GetFlightsWithDetails(FlightSearchFilter filter);

    /// <summary>
    /// Checks if a flight number already exists.
    /// </summary>
    /// <param name="flightNumber">The flight number to check.</param>
    /// <returns>True if the flight number exists, otherwise false.</returns>
    Task<bool> IsFlightNumberExistsAsync(string flightNumber);

    /// <summary>
    /// Updates a flight.
    /// </summary>
    /// <param name="flight">The flight to update.</param>
    public new void Update(Flight flight);
}

