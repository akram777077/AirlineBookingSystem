using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AirplaneRepository(ApplicationDbContext context) : GenericRepository<Airplane>(context), IAirplaneRepository
{
    public IQueryable<Airplane> GetAll()
    {
        return Context.Airplanes.AsQueryable();
    }
}
