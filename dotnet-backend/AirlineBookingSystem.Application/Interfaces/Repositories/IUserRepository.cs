using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing users.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Gets a user with their person data by username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>The user with person data, or null if not found.</returns>
    Task<User?> GetUserWithPersonAsync(string username);

    /// <summary>
    /// Searches for users based on a search filter.
    /// </summary>
    /// <param name="filter">The user search filter.</param>
    /// <returns>A queryable collection of users.</returns>
    IQueryable<User> SearchUsers(UserSearchFilter filter);
}

