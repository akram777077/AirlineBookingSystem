using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Interfaces.Repositories;

public interface IBookingStatusRepository : IGenericRepository<BookingStatus>
{
}