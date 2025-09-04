using System;
using System.Threading.Tasks;

namespace AirlineBookingSystem.Application.Interfaces.Services
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null);
        Task RemoveAsync(string key);
    }
}