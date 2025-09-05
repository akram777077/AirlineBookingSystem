using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Seat entities.
/// </summary>
public class SeatRepository(ApplicationDbContext context) : GenericRepository<Seat>(context), ISeatRepository
{
    /// <summary>
    /// Retrieves a list of available seats for a specific flight, optionally filtered by class type.
    /// </summary>
    /// <param name="flightId">The unique identifier of the flight.</param>
    /// <param name="classTypeId">Optional. The unique identifier of the class type to filter by.</param>
    /// <returns>A <see cref="Task{IReadOnlyList{Seat}}"/> representing the asynchronous operation. The task result contains a read-only list of available seats.</returns>
    public async Task<IReadOnlyList<Seat>> GetAvailableSeatsAsync(int flightId, int? classTypeId)
    {
        var query = Context.Seats
            .Include(s => s.ClassType)
            .Include(s => s.Airplane)
            .Where(s => !s.IsReserved && s.Airplane.Flights.Any(f => f.Id == flightId));

        if (classTypeId.HasValue)
        {
            query = query.Where(s => s.ClassTypesId == classTypeId.Value);
        }

        return await query.ToListAsync();
    }
}
