using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class FlightStatusRepository(ApplicationDbContext context) : GenericRepository<FlightStatus>(context), IFlightStatusRepository
{
    public new async Task<FlightStatus?> GetByIdAsync(int id)
    {
        return await Context.FlightStatuses.FirstOrDefaultAsync(fs => fs.Id == id);
    }

    public new async Task<IReadOnlyCollection<FlightStatus>> GetAllAsync()
    {
        return await Context.FlightStatuses.ToListAsync();
    }
}
