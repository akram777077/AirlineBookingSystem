using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IFlightStatusRepository : IGenericRepository<FlightStatus>
{
    Task<IReadOnlyCollection<FlightStatus>> GetAllAsync();
}
