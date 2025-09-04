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
            .Where(f => f.DeletedAt == null)
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
            
            .Include(f => f.FlightStatus)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Flight?> GetByIdAsync(int id, bool includeFlightStatus)
    {
        if (includeFlightStatus)
        {
            return await Context.Flights.Include(f => f.FlightStatus).FirstOrDefaultAsync(f => f.Id == id);
        }
        return await Context.Flights.FirstOrDefaultAsync(f => f.Id == id);
    }

    public override void Delete(Flight entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        Context.Flights.Update(entity);
    }

    public IQueryable<Flight> GetFlightsWithDetails(FlightSearchFilter filter)
    {
        var query = Context.Flights
            .AsNoTracking()
            .Where(f => f.DeletedAt == null)
            .Include(f => f.FlightStatus)
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
            .AsQueryable();

        if (filter.DepartureDate.HasValue)
            query = query.Where(f => f.DepartureTime.Date == filter.DepartureDate.Value.Date);

        if (filter.FromCityId.HasValue)
            query = query.Where(f => f.DepartureGate != null && f.DepartureGate.Terminal != null && f.DepartureGate.Terminal.Airport != null && f.DepartureGate.Terminal.Airport.City != null && f.DepartureGate.Terminal.Airport.City.Id == filter.FromCityId);

        if (filter.ToCityId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal != null && f.ArrivalGate.Terminal.Airport != null && f.ArrivalGate.Terminal.Airport.City != null && f.ArrivalGate.Terminal.Airport.City.Id == filter.ToCityId);

        if (filter.FromCountryId.HasValue)
            query = query.Where(f => f.DepartureGate != null && f.DepartureGate.Terminal != null && f.DepartureGate.Terminal.Airport != null && f.DepartureGate.Terminal.Airport.City != null && f.DepartureGate.Terminal.Airport.City.Country != null && f.DepartureGate.Terminal.Airport.City.Country.Id == filter.FromCountryId);

        if (filter.ToCountryId.HasValue)
            query = query.Where(f => f.ArrivalGate != null && f.ArrivalGate.Terminal != null && f.ArrivalGate.Terminal.Airport != null && f.ArrivalGate.Terminal.Airport.City != null && f.ArrivalGate.Terminal.Airport.City.Country != null && f.ArrivalGate.Terminal.Airport.City.Country.Id == filter.ToCountryId);

        return query;
    }

    public async Task<bool> IsFlightNumberExistsAsync(string flightNumber)
    {
        return await Context.Flights.AnyAsync(f => f.FlightNumber == flightNumber);
    }

}

