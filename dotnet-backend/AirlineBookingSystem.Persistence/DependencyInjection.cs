using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.Repositories.Generic;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories;
using AirlineBookingSystem.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineBookingSystem.Persistence;

/// <summary>
/// Extension methods for setting up persistence services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds persistence services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CONNECTION_STRING"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("CONNECTION_STRING not found in environment variables");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("AirlineBookingSystem.Persistence");
                npgsqlOptions.EnableRetryOnFailure();
            }));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IAirplaneRepository, AirplaneRepository>();
        services.AddScoped<IAirportRepository, AirportRepository>();
        services.AddScoped<IBookingStatusRepository, BookingStatusRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        
        return services;
    }
}