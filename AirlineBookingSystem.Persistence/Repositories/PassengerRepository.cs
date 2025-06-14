using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class PassengerRepository(ApplicationDbContext context)
    : GenericRepository<Passenger>(context), IPassengerRepository
{
    public async Task<Passenger?> GetWithPersonAndAddressAsync(int passengerId)
    {
        return await Context.Passengers
            .Include(p => p.User)
                .ThenInclude(u => u.Person)
                    .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == passengerId);
    }
}

