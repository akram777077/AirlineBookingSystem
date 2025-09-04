using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AirlineBookingSystem.Application.Interfaces;
using AirlineBookingSystem.Infrastructure.Services;

using AirlineBookingSystem.Application.Interfaces.Services;

namespace AirlineBookingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICacheService, RedisCacheService>();
        return services;
    }
}
