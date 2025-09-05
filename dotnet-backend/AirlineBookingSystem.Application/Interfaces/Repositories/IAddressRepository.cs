using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing addresses.
/// </summary>
public interface IAddressRepository : IGenericRepository<Address>
{
    /// <summary>
    /// Gets an address by its city ID.
    /// </summary>
    /// <param name="cityId">The ID of the city.</param>
    /// <returns>The address, or null if not found.</returns>
    Task<Address?> GetByCityIdAsync(int cityId);
}