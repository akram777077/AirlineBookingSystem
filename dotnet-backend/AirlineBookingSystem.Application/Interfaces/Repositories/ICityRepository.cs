using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface ICityRepository : IGenericRepository<City>
{
    Task<IReadOnlyCollection<City>> GetByCountryIdAsync(int countryId);
}