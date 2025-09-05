using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories.Generic;

/// <summary>
/// A generic repository for entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    /// <summary>
    /// Gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The entity, or null if not found.</returns>
    public virtual async Task<T?> GetByIdAsync(int id) 
        => await _dbSet.FindAsync(id);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A read-only list of all entities.</returns>
    public virtual async Task<IReadOnlyList<T>> GetAllAsync() 
        => await _dbSet.ToListAsync();


    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public virtual async Task AddAsync(T entity) 
        => await _dbSet.AddAsync(entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public virtual void Update(T entity) 
        => _dbSet.Update(entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public virtual void  Delete(T entity) 
        => _dbSet.Remove(entity);
}