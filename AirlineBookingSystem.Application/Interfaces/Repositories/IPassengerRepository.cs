using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IPassengerRepository : IGenericRepository<Passenger>
{
    Task<Passenger?> GetWithPersonAndAddressAsync(int passengerId);
}

