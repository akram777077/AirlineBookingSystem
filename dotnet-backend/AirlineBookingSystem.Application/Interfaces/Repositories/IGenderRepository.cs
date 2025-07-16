using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IGenderRepository : IGenericRepository<Gender>
{
    Task<Gender?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<Gender>> GetAllAsync();
}
