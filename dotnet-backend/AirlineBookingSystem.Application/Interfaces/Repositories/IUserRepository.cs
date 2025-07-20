using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserWithPersonAsync(string username);
    IQueryable<User> SearchUsers(UserSearchFilter filter);
}

