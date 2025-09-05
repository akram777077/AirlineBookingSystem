using System;
using System.Threading.Tasks;

namespace AirlineBookingSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Defines the interface for a caching service.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves a value from the cache, or creates it using the provided factory function if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve or create.</typeparam>
        /// <param name="key">The unique key to identify the cache entry.</param>
        /// <param name="factory">A function that returns a Task of the value to be cached if it's not found.</param>
        /// <param name="absoluteExpirationRelativeToNow">Optional. The time until the cache entry will expire relative to now.</param>
        /// <returns>A <see cref="Task{T}"/> representing the asynchronous operation. The task result contains the cached or newly created value.</returns>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null);

        /// <summary>
        /// Removes a value from the cache based on its key.
        /// </summary>
        /// <param name="key">The unique key of the cache entry to remove.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task RemoveAsync(string key);
    }
}