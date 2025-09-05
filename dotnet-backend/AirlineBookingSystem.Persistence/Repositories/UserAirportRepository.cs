using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing UserAirport entities.
/// Inherits from <see cref="GenericRepository{UserAirport}"/> and provides basic CRUD operations for UserAirport entities.
/// </summary>
public class UserAirportRepository(ApplicationDbContext context) : GenericRepository<UserAirport>(context), IUserAirportRepository
{
}
