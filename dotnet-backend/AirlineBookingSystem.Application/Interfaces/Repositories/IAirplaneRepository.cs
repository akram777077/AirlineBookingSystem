using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IAirplaneRepository : IGenericRepository<Airplane>
{
    IQueryable<Airplane> GetAll();
}
