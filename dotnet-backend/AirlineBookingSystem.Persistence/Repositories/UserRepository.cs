using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context)
    : GenericRepository<User>(context), IUserRepository
{
    public override async Task<User?> GetByIdAsync(int id)
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public override async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .ToListAsync();
    }

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
            query = query.Where(u => u.Person.Email.Contains(filter.Email));
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

    public async Task<User?> GetUserWithPersonAsync(string username)
    {
        return await Context.Users
            .Where(u => u.DeletedAt == null)
            .Include(u => u.Person)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public override void Delete(User entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        Context.Users.Update(entity);
    }
}

