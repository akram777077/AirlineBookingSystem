using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;

namespace AirlineBookingSystem.Persistence.Repositories;

public class CityRepository(ApplicationDbContext context)
    : GenericRepository<City>(context), ICityRepository
{
    
}

