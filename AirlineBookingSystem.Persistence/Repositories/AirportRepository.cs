using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AirportRepository(ApplicationDbContext context)
    : GenericRepository<Airport>(context), IAirportRepository
{
    public async Task<Airport?> GetByCodeAsync(string code)
    {
        return await Context.Airports
            .FirstOrDefaultAsync(a => a.AirportCode == code);
    }
}

