using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IFlightRepository : IGenericRepository<Flight>
{
    Task<IEnumerable<Flight>> SearchFlightsAsync(string fromCode, string toCode, DateTime date);
    Task<Flight?> GetFlightWithDetailsAsync(int flightId);
    Task<IEnumerable<Flight>> GetUpcomingFlightsAsync(DateTime fromDate);
}

