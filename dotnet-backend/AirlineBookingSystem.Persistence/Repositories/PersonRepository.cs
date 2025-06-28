using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

public class PersonRepository(ApplicationDbContext context)
    : GenericRepository<Person>(context), IPersonRepository
{
    
}

