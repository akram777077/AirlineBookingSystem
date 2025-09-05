using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Gate entities.
/// </summary>
public class GateRepository(ApplicationDbContext context) : GenericRepository<Gate>(context), IGateRepository
{
    /// <summary>
    /// Retrieves all gates as an IQueryable.
    /// </summary>
    /// <returns>An <see cref="IQueryable{Gate}"/> representing all gates.</returns>
    public IQueryable<Gate> GetAll() => Context.Gates.AsQueryable();
}

