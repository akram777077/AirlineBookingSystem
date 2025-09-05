using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Terminal entities.
/// </summary>
public class TerminalRepository(ApplicationDbContext context) : GenericRepository<Terminal>(context), ITerminalRepository
{
    /// <summary>
    /// Retrieves all terminals as an IQueryable.
    /// </summary>
    /// <returns>An <see cref="IQueryable{Terminal}"/> representing all terminals.</returns>
    public IQueryable<Terminal> GetAll() => Context.Terminals.AsQueryable();
}
