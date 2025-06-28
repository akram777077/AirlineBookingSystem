using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(int id) 
        => await _dbSet.FindAsync(id);

    public virtual async Task<IReadOnlyList<T>> GetAllAsync() 
        => await _dbSet.ToListAsync();


    public virtual async Task AddAsync(T entity) 
        => await _dbSet.AddAsync(entity);

    public virtual void Update(T entity) 
        => _dbSet.Update(entity);

    public virtual void  Delete(T entity) 
        => _dbSet.Remove(entity);
}