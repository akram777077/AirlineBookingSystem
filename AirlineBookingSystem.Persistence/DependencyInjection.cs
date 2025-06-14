using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Persistence.DbContext;
using AirlineBookingSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineBookingSystem.Persistence;

public static class DependencyInjection
{
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

        return services;
    }
}