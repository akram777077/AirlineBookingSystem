using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AddressRepository(ApplicationDbContext context)
    : GenericRepository<Address>(context), IAddressRepository
{
    public async Task<Address?> GetByCountryIdAsync(int countryId)
    {
        return await Context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CountryId == countryId);
    }
    public async Task<Address?> GetByCityIdAsync(int cityId)
    {
        return await Context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CityId == cityId);
    }
}

