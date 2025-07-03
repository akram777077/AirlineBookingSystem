using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
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
}

