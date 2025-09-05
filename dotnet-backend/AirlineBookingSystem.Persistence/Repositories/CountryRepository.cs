using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Country entities.
/// Inherits from <see cref="GenericRepository{Country}"/> and provides basic CRUD operations for Country entities.
/// </summary>
public class CountryRepository(ApplicationDbContext context) 
    : GenericRepository<Country>(context), ICountryRepository
{
    
}