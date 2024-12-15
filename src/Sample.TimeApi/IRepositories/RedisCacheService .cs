using StackExchange.Redis;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace Sample.TimeApi.IRepositories
{
    /// <summary>
    /// Implementation of Redis cache service using IConnectionMultiplexer.
    /// </summary>
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
        }

        /// <inheritdoc />
        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedValue, expiration);
        }

        /// <inheritdoc />
        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _database.StringGetAsync(key);
            if (serializedValue.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(serializedValue);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }

}
