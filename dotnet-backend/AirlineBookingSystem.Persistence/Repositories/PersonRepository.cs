using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Person entities.
/// Inherits from <see cref="GenericRepository{Person}"/> and provides basic CRUD operations for Person entities.
/// </summary>
public class PersonRepository(ApplicationDbContext context)
    : GenericRepository<Person>(context), IPersonRepository
{
    
}

