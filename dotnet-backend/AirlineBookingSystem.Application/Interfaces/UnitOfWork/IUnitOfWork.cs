using AirlineBookingSystem.Application.Interfaces.Repositories;

namespace AirlineBookingSystem.Application.Interfaces.UnitOfWork;

/// <summary>
/// Represents the unit of work for the application.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the address repository.
    /// </summary>
    IAddressRepository Addresses { get; }

    /// <summary>
    /// Gets the airport repository.
    /// </summary>
    IAirportRepository Airports { get; }
    
    /// <summary>
    /// Gets the booking status repository.
    /// </summary>
    IBookingStatusRepository BookingStatuses { get; }

    /// <summary>
    /// Gets the city repository.
    /// </summary>
    ICityRepository Cities { get; }

    /// <summary>
    /// Gets the country repository.
    /// </summary>
    ICountryRepository Countries { get; }

    /// <summary>
    /// Gets the flight repository.
    /// </summary>
    IFlightRepository Flights { get; }

    /// <summary>
    /// Gets the person repository.
    /// </summary>
    IPersonRepository People { get; }

    /// <summary>
    /// Gets the role repository.
    /// </summary>
    IRoleRepository Roles { get; }

    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository Users { get; }

    /// <summary>
    /// Gets the airplane repository.
    /// </summary>
    IAirplaneRepository Airplanes { get; }

    /// <summary>
    /// Gets the gate repository.
    /// </summary>
    IGateRepository Gates { get; }

    /// <summary>
    /// Gets the flight status repository.
    /// </summary>
    IFlightStatusRepository FlightStatuses { get; }

    /// <summary>
    /// Gets the seat repository.
    /// </summary>
    ISeatRepository Seats { get; }
    
    /// <summary>
    /// Gets the terminal repository.
    /// </summary>
    ITerminalRepository Terminals { get; }

    /// <summary>
    /// Gets the class type repository.
    /// </summary>
    IClassTypeRepository ClassTypes { get; }

    /// <summary>
    /// Gets the flight class repository.
    /// </summary>
    IFlightClassRepository FlightClasses { get; }

    /// <summary>
    /// Gets the role permission repository.
    /// </summary>
    IRolePermissionRepository RolePermissions { get; }

    /// <summary>
    /// Gets the permission repository.
    /// </summary>
    IPermissionRepository Permissions { get; }

    /// <summary>
    /// Gets the gender repository.
    /// </summary>
    IGenderRepository Genders { get; }

    /// <summary>
    /// Gets the user airport repository.
    /// </summary>
    IUserAirportRepository UserAirports { get; }

    /// <summary>
    /// Completes the unit of work, saving all changes to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> CompleteAsync();
}