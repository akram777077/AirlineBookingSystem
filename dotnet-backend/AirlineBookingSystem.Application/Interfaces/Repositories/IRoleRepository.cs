using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    new Task<IReadOnlyCollection<Role>> GetAllAsync();
    new Task<Role?> GetByIdAsync(int id);
}
