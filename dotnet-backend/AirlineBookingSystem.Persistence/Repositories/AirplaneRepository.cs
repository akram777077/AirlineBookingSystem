using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Airplane entities.
/// </summary>
public class AirplaneRepository(ApplicationDbContext context) : GenericRepository<Airplane>(context), IAirplaneRepository
{
    /// <summary>
    /// Retrieves all airplanes as an IQueryable.
    /// </summary>
    /// <returns>An <see cref="IQueryable{Airplane}"/> representing all airplanes.</returns>
    public IQueryable<Airplane> GetAll()
    {
        return Context.Airplanes.AsQueryable();
    }
}
