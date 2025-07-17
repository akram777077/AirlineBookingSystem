using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IClassTypeRepository : IGenericRepository<ClassType>
{
    new Task<ClassType?> GetByIdAsync(int id);
    new Task<IReadOnlyCollection<ClassType>> GetAllAsync();
}
