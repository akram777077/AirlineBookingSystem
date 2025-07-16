using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class GenderRepository(ApplicationDbContext context) : GenericRepository<Gender>(context), IGenderRepository
{
    public async Task<IReadOnlyCollection<Gender>> GetAllAsync()
    {
        return await Context.Genders.ToListAsync();
    }
}
