using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface ISeatRepository : IGenericRepository<Seat>
{
    Task<IReadOnlyList<Seat>> GetAvailableSeatsAsync(int flightId, int? classTypeId);
}
