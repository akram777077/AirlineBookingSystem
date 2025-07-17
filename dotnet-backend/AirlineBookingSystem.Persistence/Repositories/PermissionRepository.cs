using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class PermissionRepository(ApplicationDbContext context) : GenericRepository<Permission>(context), IPermissionRepository
{
    public new async Task<IReadOnlyCollection<Permission>> GetAllAsync()
    {
        return await Context.Permissions.ToListAsync();
    }

    public new async Task<Permission?> GetByIdAsync(int id)
    {
        return await Context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
    }
}

