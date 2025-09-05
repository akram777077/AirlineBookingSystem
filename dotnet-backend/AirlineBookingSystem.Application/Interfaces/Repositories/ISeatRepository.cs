using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing seats.
/// </summary>
public interface ISeatRepository : IGenericRepository<Seat>
{
    /// <summary>
    /// Gets a read-only list of available seats for a given flight and optional class type.
    /// </summary>
    /// <param name="flightId">The ID of the flight.</param>
    /// <param name="classTypeId">The optional ID of the class type.</param>
    /// <returns>A read-only list of available seats.</returns>
    Task<IReadOnlyList<Seat>> GetAvailableSeatsAsync(int flightId, int? classTypeId);
}
