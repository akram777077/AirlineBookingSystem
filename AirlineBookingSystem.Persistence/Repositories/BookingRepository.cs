using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Persistence.Repositories;

public class BookingRepository(ApplicationDbContext context, IBookingStatusRepository bookingStatusRepository)
    : GenericRepository<Booking>(context), IBookingRepository
{

}
