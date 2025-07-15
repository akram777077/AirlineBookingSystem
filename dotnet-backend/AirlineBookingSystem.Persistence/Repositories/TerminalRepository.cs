using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AirlineBookingSystem.Persistence.Repositories;

public class TerminalRepository(ApplicationDbContext context)
    : GenericRepository<Terminal>(context), ITerminalRepository
{
    public IQueryable<Terminal> GetAll()
    {
        return Context.Terminals.Include(t => t.Airport).AsQueryable();
    }
}