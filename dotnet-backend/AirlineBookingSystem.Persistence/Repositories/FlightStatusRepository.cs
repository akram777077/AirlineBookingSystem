using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class FlightStatusRepository(ApplicationDbContext context) : GenericRepository<FlightStatus>(context), IFlightStatusRepository
{
    public async Task<IReadOnlyCollection<FlightStatus>> GetAllAsync()
    {
        return await Context.FlightStatuses.ToListAsync();
    }
}
