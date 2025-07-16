using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IClassTypeRepository : IGenericRepository<ClassType>
{
    Task<ClassType?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<ClassType>> GetAllAsync();
}
