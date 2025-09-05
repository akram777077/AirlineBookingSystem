using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing ClassType entities.
/// </summary>
public class ClassTypeRepository(ApplicationDbContext context) : GenericRepository<ClassType>(context), IClassTypeRepository
{
    /// <summary>
    /// Retrieves a class type by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the class type.</param>
    /// <returns>A <see cref="Task{ClassType}"/> representing the asynchronous operation. The task result contains the class type if found, otherwise null.</returns>
    public new async Task<ClassType?> GetByIdAsync(int id)
    {
        return await Context.ClassTypes.FirstOrDefaultAsync(ct => ct.Id == id);
    }

    /// <summary>
    /// Retrieves all class types.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{ClassType}}"/> representing the asynchronous operation. The task result contains a read-only collection of all class types.</returns>
    public new async Task<IReadOnlyCollection<ClassType>> GetAllAsync()
    {
        return await Context.ClassTypes.ToListAsync();
    }
}
