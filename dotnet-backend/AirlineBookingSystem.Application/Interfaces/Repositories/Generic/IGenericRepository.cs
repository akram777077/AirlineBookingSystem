namespace AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

/// <summary>
/// Represents a generic repository for entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IGenericRepository <T> where T : class
{
    /// <summary>
    /// Gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The entity, or null if not found.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A read-only list of all entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);
}