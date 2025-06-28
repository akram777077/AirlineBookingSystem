using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IAirportRepository : IGenericRepository<Airport>
{
    Task<Airport?> GetByCodeAsync(string code);
    Task<List<Airport>> GetByCityIdAsync(int cityId);
}