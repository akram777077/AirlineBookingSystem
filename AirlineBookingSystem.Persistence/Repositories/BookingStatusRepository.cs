using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class BookingStatusRepository(ApplicationDbContext context)
    : GenericRepository<BookingStatus>(context), IBookingStatusRepository
{
    public async Task<BookingStatus?> GetByStatusEnumAsync(BookingStatusEnum statusEnum)
        => await Context.BookingStatuses.FirstOrDefaultAsync(b => b.StatusName == statusEnum);
}
