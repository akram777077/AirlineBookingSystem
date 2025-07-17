using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IFlightStatusRepository : IGenericRepository<FlightStatus>
{
    new Task<FlightStatus?> GetByIdAsync(int id);
    new Task<IReadOnlyCollection<FlightStatus>> GetAllAsync();
}
