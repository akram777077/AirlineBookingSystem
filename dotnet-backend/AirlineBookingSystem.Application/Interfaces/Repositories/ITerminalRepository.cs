using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface ITerminalRepository : IGenericRepository<Terminal>
{
    IQueryable<Terminal> GetAll();
}