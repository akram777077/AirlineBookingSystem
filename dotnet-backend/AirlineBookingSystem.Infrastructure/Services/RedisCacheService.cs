using System;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineBookingSystem.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace AirlineBookingSystem.Infrastructure.Services
{
    /// <summary>
    /// A cache service implementation using Redis.
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheService"/> class.
        /// </summary>
        /// <param name="cache">The distributed cache.</param>
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Gets a value from the cache, or creates it if it does not exist.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="factory">The factory function to create the value if it does not exist.</param>
        /// <param name="absoluteExpirationRelativeToNow">The absolute expiration relative to now.</param>
        /// <returns>The cached or created value.</returns>
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (cachedValue != null)
            {
                return JsonSerializer.Deserialize<T>(cachedValue)!;
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

        /// <summary>
        /// Removes a value from the cache.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}