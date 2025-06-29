using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories;

namespace AirlineBookingSystem.Persistence.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public IAddressRepository Addresses { get; } = new AddressRepository(context);
    public IAirportRepository Airports { get; } = new AirportRepository(context);
    public IBookingRepository Bookings { get; } = new BookingRepository(context);
    public IBookingStatusRepository BookingStatuses { get; } = new BookingStatusRepository(context);
    public ICityRepository Cities { get; } = new CityRepository(context);
    public ICountryRepository Countries { get; } = new CountryRepository(context);
    public IFlightRepository Flights { get; } = new FlightRepository(context);
    public IPersonRepository People { get; } = new PersonRepository(context);
    public IRoleRepository Roles { get; } = new RoleRepository(context);
    public IUserRepository Users { get; } = new UserRepository(context);

    public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
}