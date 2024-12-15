using System.Threading.Tasks;
using System;

namespace Sample.TimeApi.IRepositories
{
    /// <summary>
    /// Defines methods for interacting with Redis cache.
    /// </summary>
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
