using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class SeatRepository(ApplicationDbContext context) : GenericRepository<Seat>(context), ISeatRepository
{
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
