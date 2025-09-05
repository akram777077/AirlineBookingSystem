using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Address entities.
/// </summary>
public class AddressRepository(ApplicationDbContext context)
    : GenericRepository<Address>(context), IAddressRepository
{
    /// <summary>
    /// Retrieves an address by its associated city ID.
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <returns>A <see cref="Task{Address}"/> representing the asynchronous operation. The task result contains the address if found, otherwise null.</returns>
    public async Task<Address?> GetByCityIdAsync(int cityId)
    {
        return await Context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CityId == cityId);
    }
}

