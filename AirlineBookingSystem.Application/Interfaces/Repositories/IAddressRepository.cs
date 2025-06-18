using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address?> GetByCountryIdAsync(int countryId);
    Task<Address?> GetByCityIdAsync(int cityId);
}