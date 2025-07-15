using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IAirportRepository : IGenericRepository<Airport>
{
    Task<Airport?> GetByCodeAsync(string code);
    Task<List<Airport>> GetByCityIdAsync(int cityId);
    IQueryable<Airport> GetAll();
}