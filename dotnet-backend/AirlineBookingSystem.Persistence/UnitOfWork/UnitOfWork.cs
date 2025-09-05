using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories;

namespace AirlineBookingSystem.Persistence.UnitOfWork;

/// <summary>
/// The unit of work for the application.
/// </summary>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public IAddressRepository Addresses { get; } = new AddressRepository(context);
    public IAirportRepository Airports { get; } = new AirportRepository(context);
    
    public IBookingStatusRepository BookingStatuses { get; } = new BookingStatusRepository(context);
    public ICityRepository Cities { get; } = new CityRepository(context);
    public ICountryRepository Countries { get; } = new CountryRepository(context);
    public IFlightRepository Flights { get; } = new FlightRepository(context);
    public IPersonRepository People { get; } = new PersonRepository(context);
    public IRoleRepository Roles { get; } = new RoleRepository(context);
    public IUserRepository Users { get; } = new UserRepository(context);
    public IAirplaneRepository Airplanes { get; } = new AirplaneRepository(context);
    public IGateRepository Gates { get; } = new GateRepository(context);
    public IFlightStatusRepository FlightStatuses { get; } = new FlightStatusRepository(context);
    public ISeatRepository Seats { get; } = new SeatRepository(context);
    
    public ITerminalRepository Terminals { get; } = new TerminalRepository(context);
    public IClassTypeRepository ClassTypes { get; } = new ClassTypeRepository(context);
    public IFlightClassRepository FlightClasses { get; } = new FlightClassRepository(context);
    public IRolePermissionRepository RolePermissions { get; } = new RolePermissionRepository(context);
    public IPermissionRepository Permissions { get; } = new PermissionRepository(context);
    public IGenderRepository Genders { get; } = new GenderRepository(context);
    public IUserAirportRepository UserAirports { get; } = new UserAirportRepository(context);

    /// <summary>
    /// Completes the unit of work by saving changes to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
}