using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using AirlineBookingSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

/// <summary>
/// Repository for managing BookingStatus entities.
/// </summary>
public class BookingStatusRepository(ApplicationDbContext context)
    : GenericRepository<BookingStatus>(context), IBookingStatusRepository
{
    /// <summary>
    /// Retrieves a booking status by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the booking status.</param>
    /// <returns>A <see cref="Task{BookingStatus}"/> representing the asynchronous operation. The task result contains the booking status if found, otherwise null.</returns>
    public new async Task<BookingStatus?> GetByIdAsync(int id)
    {
        return await Context.BookingStatuses.FirstOrDefaultAsync(bs => bs.Id == id);
    }

    /// <summary>
    /// Retrieves all booking statuses.
    /// </summary>
    /// <returns>A <see cref="Task{IReadOnlyCollection{BookingStatus}}"/> representing the asynchronous operation. The task result contains a read-only collection of all booking statuses.</returns>
    public new async Task<IReadOnlyCollection<BookingStatus>> GetAllAsync()
    {
        return await Context.BookingStatuses.ToListAsync();
    }
}
