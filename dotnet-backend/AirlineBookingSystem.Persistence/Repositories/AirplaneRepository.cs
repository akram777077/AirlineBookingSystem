using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class AirplaneRepository(ApplicationDbContext context) : GenericRepository<Airplane>(context), IAirplaneRepository
{
    public IQueryable<Airplane> GetAll()
    {
        return Context.Airplanes.AsQueryable();
    }

    public async Task<Airplane> GetByCodeAsync(string code)
    {
        return await Context.Airplanes.FirstOrDefaultAsync(a => a.Code == code);
    }
}
