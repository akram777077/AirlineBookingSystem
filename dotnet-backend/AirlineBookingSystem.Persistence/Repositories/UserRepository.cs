using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing User entities, including soft deletion and detailed retrieval.
/// </summary>
public class UserRepository(ApplicationDbContext context)
    : GenericRepository<User>(context), IUserRepository
{
    /// <summary>
    /// Retrieves a user by their ID, excluding soft-deleted users.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A <see cref="Task{User}"/> representing the asynchronous operation. The task result contains the user if found, otherwise null.</returns>
    public override async Task<User?> GetByIdAsync(int id)
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Retrieves all users, excluding soft-deleted users.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyList{User}}"/> representing the asynchronous operation. The task result contains a read-only list of all users.</returns>
    public override async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .ToListAsync();
    }

    /// <summary>
    /// Searches for users based on various criteria and provides an IQueryable for further filtering/pagination.
    /// Soft-deleted users are excluded from the search results.
    /// </summary>
    /// <param name="filter">The <see cref="UserSearchFilter"/> containing search parameters such as username, email, active status, and role ID.</param>
    /// <returns>An <see cref="IQueryable{User}"/> representing the filtered users with their associated person and role details.</returns>
    public IQueryable<User> SearchUsers(UserSearchFilter filter)
    {
        var query = Context.Users
            .Include(u => u.Person)
            .Include(u => u.Role)
            .Where(u => u.DeletedAt == null)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Username))
        {
            query = query.Where(u => u.Username.Contains(filter.Username));
        }

        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(u => u.Person != null && u.Person.Email != null && u.Person.Email.Contains(filter.Email));
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(u => u.IsActive == filter.IsActive.Value);
        }

        if (filter.RoleId.HasValue)
        {
            query = query.Where(u => u.RoleId == filter.RoleId.Value);
        }

        return query;
    }

    /// <summary>
    /// Retrieves a user by username, including their associated person details, excluding soft-deleted users.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A <see cref="Task{User}"/> representing the asynchronous operation. The task result contains the user with person details if found, otherwise null.</returns>
    public async Task<User?> GetUserWithPersonAsync(string username)
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .Include(u => u.Person)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Soft-deletes a user by setting their <see cref="User.DeletedAt"/> timestamp.
    /// This method overrides the base <see cref="GenericRepository{T}.Delete"/> method to implement soft deletion.
    /// </summary>
    /// <param name="entity">The user entity to soft-delete.</param>
    public override void Delete(User entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        Context.Users.Update(entity);
    }
}

