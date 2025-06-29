using AirlineBookingSystem.Shared.DTOs.Flights;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Services;

public interface IFlightService
{
    Task<IEnumerable<Flight>> SearchFlightsAsync(string fromCode, string toCode, DateTime date);
    Task<Flight?> GetByIdAsync(int flightId);
    Task<IEnumerable<Flight>> GetUpcomingFlightsAsync(DateTime fromDate);
    Task<IReadOnlyList<Flight>> GetAllAsync();
}
