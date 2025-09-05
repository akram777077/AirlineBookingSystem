using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing FlightClass entities.
/// Inherits from <see cref="GenericRepository{FlightClass}"/> and provides basic CRUD operations for FlightClass entities.
/// </summary>
public class FlightClassRepository(ApplicationDbContext context) : GenericRepository<FlightClass>(context), IFlightClassRepository
{
}
