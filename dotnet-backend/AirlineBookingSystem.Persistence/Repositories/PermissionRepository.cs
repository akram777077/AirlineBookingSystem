using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Permission entities.
/// </summary>
public class PermissionRepository(ApplicationDbContext context) : GenericRepository<Permission>(context), IPermissionRepository
{
    /// <summary>
    /// Retrieves all permissions.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{Permission}}"/> representing the asynchronous operation. The task result contains a read-only collection of all permissions.</returns>
    public new async Task<IReadOnlyCollection<Permission>> GetAllAsync()
    {
        return await Context.Permissions.ToListAsync();
    }

    /// <summary>
    /// Retrieves a permission by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the permission.</param>
    /// <returns>A <see cref="Task{Permission}"/> representing the asynchronous operation. The task result contains the permission if found, otherwise null.</returns>
    public new async Task<Permission?> GetByIdAsync(int id)
    {
        return await Context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
    }
}

