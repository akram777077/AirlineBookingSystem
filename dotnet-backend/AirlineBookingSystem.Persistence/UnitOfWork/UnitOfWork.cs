using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Persistence.DbContext;

namespace AirlineBookingSystem.Persistence.UnitOfWork;

public class UnitOfWork(
    ApplicationDbContext context,
    IAddressRepository addresses,
    IAirportRepository airports,
    IBookingRepository bookings,
    IBookingStatusRepository bookingStatuses,
    ICityRepository cities,
    ICountryRepository countries,
    IFlightRepository flights,
    IPersonRepository people,
    IRoleRepository roles,
    IUserRepository users) : IUnitOfWork
{
    public IAddressRepository Addresses { get; } = addresses;
    public IAirportRepository Airports { get; } = airports;
    public IBookingRepository Bookings { get; } = bookings;
    public IBookingStatusRepository BookingStatuses { get; } = bookingStatuses;
    public ICityRepository Cities { get; } = cities;
    public ICountryRepository Countries { get; } = countries;
    public IFlightRepository Flights { get; } = flights;
    public IPersonRepository People { get; } = people;
    public IRoleRepository Roles { get; } = roles;
    public IUserRepository Users { get; } = users;

    public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
}