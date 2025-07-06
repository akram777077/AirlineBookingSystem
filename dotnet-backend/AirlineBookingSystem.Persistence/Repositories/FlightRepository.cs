using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using AirlineBookingSystem.Shared.Filters;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class FlightRepository(ApplicationDbContext context)
    : GenericRepository<Flight>(context), IFlightRepository
{
    public override Task<Flight?> GetByIdAsync(int id)
    {
        return Context.Flights
            .Include(f => f.Airplane)
            .Include(f => f.DepartureGate)
            .ThenInclude(g => g.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Include(f => f.ArrivalGate)
            .ThenInclude(g => g!.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Include(f => f.Bookings)
            .Include(f => f.FlightStatus)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IReadOnlyList<Flight>> GetFlightsWithDetailsAsync(FlightSearchFilter filter)
    {
        var query = Context.Flights
            .AsNoTracking()
            .Include(f => f.FlightStatus)
            .Include(f => f.Airplane)
            .Include(f => f.DepartureGate)
            .ThenInclude(g => g.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Include(f => f.ArrivalGate)
            .ThenInclude(g => g.Terminal)
            .ThenInclude(t => t.Airport)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .AsQueryable();

        if (filter.DepartureDate.HasValue)
            query = query.Where(f => f.DepartureTime.Date == filter.DepartureDate.Value.Date);

        if (filter.FromCityId.HasValue)
            query = query.Where(f => f.DepartureGate.Terminal.Airport.City.Id == filter.FromCityId);

        if (filter.ToCityId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal.Airport.City.Id == filter.ToCityId);

        if (filter.FromCountryId.HasValue)
            query = query.Where(f => f.DepartureGate.Terminal.Airport.City.Country.Id == filter.FromCountryId);

        if (filter.ToCountryId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal.Airport.City.Country.Id == filter.ToCountryId);

        return await query.ToListAsync();
    }

    public async Task<bool> IsFlightNumberExistsAsync(string flightNumber)
    {
        return await Context.Flights.AnyAsync(f => f.FlightNumber == flightNumber);
    }

}

