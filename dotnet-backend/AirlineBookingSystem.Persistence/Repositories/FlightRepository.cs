using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using AirlineBookingSystem.Shared.DTOs.Flights;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class FlightRepository(ApplicationDbContext context)
    : GenericRepository<Flight>(context), IFlightRepository
{
    public async Task<IEnumerable<Flight>> SearchFlightsAsync(string fromCode, string toCode, DateTime date)
    {
        return await Context.Flights
            .Include(f => f.FromAirport).ThenInclude(a => a.City)
            .Include(f => f.ToAirport).ThenInclude(a => a.City)
            .Where(f =>
                f.FromAirport.AirportCode == fromCode &&
                f.ToAirport.AirportCode == toCode &&
                f.DepartureTime == date.Date)
            .ToListAsync();
    }
    
    public override async Task<Flight?> GetByIdAsync(int flightId)
    {
        return await Context.Flights
            .Include(f => f.FromAirport).ThenInclude(a => a.City)
            .Include(f => f.ToAirport).ThenInclude(a => a.City)
            .FirstOrDefaultAsync(f => f.Id == flightId);
    }

    public async Task<IEnumerable<Flight>> GetUpcomingFlightsAsync(DateTime fromDate)
    {
        return await Context.Flights
            .Where(f => f.DepartureTime != null && f.DepartureTime.Value > fromDate)
            .OrderBy(f => f.DepartureTime)
            .ToListAsync();
    }

    public override async Task<IReadOnlyList<Flight>> GetAllAsync()
    {
        return await Context.Flights
            .Include(f => f.FromAirport).ThenInclude(a => a.City)
            .Include(f => f.ToAirport).ThenInclude(a => a.City)
            .ToListAsync();
    }
}

