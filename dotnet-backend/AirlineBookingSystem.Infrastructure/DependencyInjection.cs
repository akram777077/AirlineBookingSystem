using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AirlineBookingSystem.Application.Interfaces;
using AirlineBookingSystem.Infrastructure.Services;

using AirlineBookingSystem.Application.Interfaces.Services;

namespace AirlineBookingSystem.Infrastructure;

/// <summary>
/// Extension methods for setting up infrastructure services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICacheService, RedisCacheService>();
        return services;
    }
}
