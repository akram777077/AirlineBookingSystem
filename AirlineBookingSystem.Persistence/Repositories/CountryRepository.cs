using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;

namespace AirlineBookingSystem.Persistence.Repositories;

public class CountryRepository(ApplicationDbContext context) 
    : GenericRepository<Country>(context), ICountryRepository
{
    
}