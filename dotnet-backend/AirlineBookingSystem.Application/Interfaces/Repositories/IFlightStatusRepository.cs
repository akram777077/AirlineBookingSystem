using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IFlightStatusRepository : IGenericRepository<FlightStatus>
{
    Task<FlightStatus?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<FlightStatus>> GetAllAsync();
}
