using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using System.Linq;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing airplanes.
/// </summary>
public interface IAirplaneRepository : IGenericRepository<Airplane>
{
    /// <summary>
    /// Gets all airplanes as a queryable collection.
    /// </summary>
    /// <returns>A queryable collection of all airplanes.</returns>
    IQueryable<Airplane> GetAll();
}
