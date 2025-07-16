using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IGenderRepository : IGenericRepository<Gender>
{
    Task<IReadOnlyCollection<Gender>> GetAllAsync();
}
