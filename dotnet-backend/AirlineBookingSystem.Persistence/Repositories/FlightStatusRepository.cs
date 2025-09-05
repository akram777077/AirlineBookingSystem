using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing FlightStatus entities.
/// </summary>
public class FlightStatusRepository(ApplicationDbContext context) : GenericRepository<FlightStatus>(context), IFlightStatusRepository
{
    /// <summary>
    /// Retrieves a flight status by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the flight status.</param>
    /// <returns>A <see cref="Task{FlightStatus}"/> representing the asynchronous operation. The task result contains the flight status if found, otherwise null.</returns>
    public new async Task<FlightStatus?> GetByIdAsync(int id)
    {
        return await Context.FlightStatuses.FirstOrDefaultAsync(fs => fs.Id == id);
    }

    /// <summary>
    /// Retrieves all flight statuses.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{FlightStatus}}"/> representing the asynchronous operation. The task result contains a read-only collection of all flight statuses.</returns>
    public new async Task<IReadOnlyCollection<FlightStatus>> GetAllAsync()
    {
        return await Context.FlightStatuses.ToListAsync();
    }
}
