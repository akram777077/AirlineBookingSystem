using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing countries.
/// </summary>
public interface ICountryRepository : IGenericRepository<Country>
{
    
}