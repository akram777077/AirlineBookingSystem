using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IBookingStatusRepository : IGenericRepository<BookingStatus>
{
    new Task<BookingStatus?> GetByIdAsync(int id);
}