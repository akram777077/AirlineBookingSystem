using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using AirlineBookingSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class BookingStatusRepository(ApplicationDbContext context)
    : GenericRepository<BookingStatus>(context), IBookingStatusRepository
{
    public async Task<BookingStatus?> GetByIdAsync(int id)
    {
        return await Context.BookingStatuses.FirstOrDefaultAsync(bs => bs.Id == id);
    }

    public async Task<IReadOnlyCollection<BookingStatus>> GetAllAsync()
    {
        return await Context.BookingStatuses.ToListAsync();
    }
}
