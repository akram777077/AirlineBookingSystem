using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AirportRepository(ApplicationDbContext context)
    : GenericRepository<Airport>(context), IAirportRepository
{
    public async Task<Airport?> GetByCodeAsync(string code)
    {
        return await Context.Airports
            .FirstOrDefaultAsync(a => a.AirportCode == code);
    }

    public async Task<List<Airport>> GetByCityIdAsync(int cityId)
    {
        return await Context.Airports
            .Include(a => a.City)
            .Where(a=>a.CityId == cityId)
            .ToListAsync();
    }

    public IQueryable<Airport> GetAll()
    {
        return Context.Airports.Include(a => a.City).AsQueryable();
    }
}

