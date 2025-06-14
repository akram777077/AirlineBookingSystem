using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AddressRepository(ApplicationDbContext context)
    : GenericRepository<Address>(context), IAddressRepository
{
    
}

