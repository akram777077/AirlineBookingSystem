using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface ICityRepository : IGenericRepository<City>
{
    IQueryable<City> GetAll();
    Task<IReadOnlyCollection<City>> GetByCountryIdAsync(int countryId);
}