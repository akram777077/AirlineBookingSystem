using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing terminals.
/// </summary>
public interface ITerminalRepository : IGenericRepository<Terminal>
{
    /// <summary>
    /// Gets all terminals as a queryable collection.
    /// </summary>
    /// <returns>A queryable collection of all terminals.</returns>
    IQueryable<Terminal> GetAll();
}