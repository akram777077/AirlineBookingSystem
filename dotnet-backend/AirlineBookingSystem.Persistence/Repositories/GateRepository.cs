using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

public class GateRepository(ApplicationDbContext context) : GenericRepository<Gate>(context), IGateRepository
{
    public IQueryable<Gate> GetAll() => Context.Gates.AsQueryable();
}
