using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IGenderRepository : IGenericRepository<Gender>
{
    new Task<Gender?> GetByIdAsync(int id);
    new Task<IReadOnlyCollection<Gender>> GetAllAsync();
}
