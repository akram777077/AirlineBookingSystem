using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class ClassTypeRepository(ApplicationDbContext context) : GenericRepository<ClassType>(context), IClassTypeRepository
{
    public new async Task<ClassType?> GetByIdAsync(int id)
    {
        return await Context.ClassTypes.FirstOrDefaultAsync(ct => ct.Id == id);
    }

    public new async Task<IReadOnlyCollection<ClassType>> GetAllAsync()
    {
        return await Context.ClassTypes.ToListAsync();
    }
}
