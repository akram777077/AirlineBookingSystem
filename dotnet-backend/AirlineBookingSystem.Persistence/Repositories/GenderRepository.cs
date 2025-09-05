using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing Gender entities.
/// </summary>
public class GenderRepository(ApplicationDbContext context) : GenericRepository<Gender>(context), IGenderRepository
{
    /// <summary>
    /// Retrieves a gender by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the gender.</param>
    /// <returns>A <see cref="Task{Gender}"/> representing the asynchronous operation. The task result contains the gender if found, otherwise null.</returns>
    public new async Task<Gender?> GetByIdAsync(int id)
    {
        return await Context.Genders.FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <summary>
    /// Retrieves all genders.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{Gender}}"/> representing the asynchronous operation. The task result contains a read-only collection of all genders.</returns>
    public new async Task<IReadOnlyCollection<Gender>> GetAllAsync()
    {
        return await Context.Genders.ToListAsync();
    }
}
