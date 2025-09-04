using System;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineBookingSystem.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace AirlineBookingSystem.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (cachedValue != null)
            {
                return JsonSerializer.Deserialize<T>(cachedValue);
            }

            var value = await factory();
            if (value != null)
            {
                var options = new DistributedCacheEntryOptions();
                if (absoluteExpirationRelativeToNow.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow.Value;
                }
                else
                {
                    options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Default cache duration
                }
                await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
            }
            return value;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}