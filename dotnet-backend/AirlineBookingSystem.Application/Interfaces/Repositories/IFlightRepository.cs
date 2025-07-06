using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IFlightRepository : IGenericRepository<Flight>
{
    public Task<IReadOnlyList<Flight>> GetFlightsWithDetailsAsync(FlightSearchFilter filter);
    Task<bool> IsFlightNumberExistsAsync(string flightNumber);
    public new void Update(Flight flight);
}

