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
    IAirplaneRepository Airplanes { get; }
    IGateRepository Gates { get; }
    IFlightStatusRepository FlightStatuses { get; }
    ISeatRepository Seats { get; }
    IPaymentRepository Payments { get; }
    ITerminalRepository Terminals { get; }
    IClassTypeRepository ClassTypes { get; }
    IFlightClassRepository FlightClasses { get; }
    IRolePermissionRepository RolePermissions { get; }
    IPermissionRepository Permissions { get; }
    IGenderRepository Genders { get; }
    IUserAirportRepository UserAirports { get; }

    Task<int> CompleteAsync();
}