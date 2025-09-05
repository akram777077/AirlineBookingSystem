using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing flight statuses.
/// </summary>
public interface IFlightStatusRepository : IGenericRepository<FlightStatus>
{
    /// <summary>
    /// Gets a flight status by its ID.
    /// </summary>
    /// <param name="id">The ID of the flight status.</param>
    /// <returns>The flight status, or null if not found.</returns>
    new Task<FlightStatus?> GetByIdAsync(int id);

    /// <summary>
    /// Gets a read-only collection of all flight statuses.
    /// </summary>
    /// <returns>A read-only collection of all flight statuses.</returns>
    new Task<IReadOnlyCollection<FlightStatus>> GetAllAsync();
}
