using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class CityRepository(ApplicationDbContext context)
    : GenericRepository<City>(context), ICityRepository
{
    public async Task<IReadOnlyCollection<City>> GetByCountryIdAsync(int countryId)
    {
        return await Context.Cities
            .Where(city => city.CountryId == countryId)
            .ToListAsync();
    }
}

