using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing booking statuses.
/// </summary>
public interface IBookingStatusRepository : IGenericRepository<BookingStatus>
{
    /// <summary>
    /// Gets a booking status by its ID.
    /// </summary>
    /// <param name="id">The ID of the booking status.</param>
    /// <returns>The booking status, or null if not found.</returns>
    new Task<BookingStatus?> GetByIdAsync(int id);
}