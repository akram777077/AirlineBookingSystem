using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IPermissionRepository : IGenericRepository<Permission>
{
    new Task<IReadOnlyCollection<Permission>> GetAllAsync();
    new Task<Permission?> GetByIdAsync(int id);
}

