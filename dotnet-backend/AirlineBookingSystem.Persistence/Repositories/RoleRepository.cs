using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Role entities.
/// </summary>
public class RoleRepository(ApplicationDbContext context) : GenericRepository<Role>(context), IRoleRepository
{
    /// <summary>
    /// Retrieves all roles.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{Role}}"/> representing the asynchronous operation. The task result contains a read-only collection of all roles.</returns>
    public new async Task<IReadOnlyCollection<Role>> GetAllAsync()
    {
        return await Context.Roles.ToListAsync();
    }

    /// <summary>
    /// Retrieves a role by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <returns>A <see cref="Task{Role}"/> representing the asynchronous operation. The task result contains the role if found, otherwise null.</returns>
    public new async Task<Role?> GetByIdAsync(int id)
    {
        return await Context.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }
}

