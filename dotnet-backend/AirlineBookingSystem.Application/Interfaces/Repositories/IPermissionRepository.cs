using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IPermissionRepository : IGenericRepository<Permission>
{
    Task<IReadOnlyCollection<Permission>> GetAllAsync();
    Task<Permission?> GetByIdAsync(int id);
}

