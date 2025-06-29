using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;

namespace AirlineBookingSystem.Application.Services;

public class FlightService(IUnitOfWork unitOfWork) : IFlightService
{
    public async Task<IEnumerable<Flight>> SearchFlightsAsync(string fromCode, string toCode, DateTime date)
        => await unitOfWork.Flights.SearchFlightsAsync(fromCode, toCode, date);

    public async Task<Flight?> GetByIdAsync(int flightId)
        => await unitOfWork.Flights.GetByIdAsync(flightId);

    public async Task<IEnumerable<Flight>> GetUpcomingFlightsAsync(DateTime fromDate)
        => await unitOfWork.Flights.GetUpcomingFlightsAsync(fromDate);

    public async Task<IReadOnlyList<Flight>> GetAllAsync()
        => await unitOfWork.Flights.GetAllAsync();
}
