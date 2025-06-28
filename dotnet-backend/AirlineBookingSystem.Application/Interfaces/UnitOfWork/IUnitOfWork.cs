using AirlineBookingSystem.Application.Interfaces.Repositories;

namespace AirlineBookingSystem.Application.Interfaces.UnitOfWork;


public interface IUnitOfWork
{
    IAddressRepository Addresses { get; }
    IAirportRepository Airports { get; }
    IBookingRepository Bookings { get; }
    IBookingStatusRepository BookingStatuses { get; }
    ICityRepository Cities { get; }
    ICountryRepository Countries { get; }
    IFlightRepository Flights { get; }
    IPersonRepository People { get; }
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }

    Task<int> CompleteAsync();
}