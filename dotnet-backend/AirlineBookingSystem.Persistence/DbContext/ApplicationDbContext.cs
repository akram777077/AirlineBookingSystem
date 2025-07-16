using Microsoft.EntityFrameworkCore;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Persistence.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)


{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingStatus> BookingStatuses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<Terminal> Terminals { get; set; }
    public DbSet<Gate> Gates { get; set; }
    public DbSet<ClassType> ClassTypes { get; set; }
    public DbSet<FlightStatus> FlightStatuses { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}