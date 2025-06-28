using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address?> GetByCityIdAsync(int cityId);
}