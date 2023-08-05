using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Redis.Connection;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Crawler.Infra.Redis.Repositories;

internal class CacheRepository : ICacheRepository
{
    private readonly IDatabase _database;
    private readonly RedisSettings _settings;
    private readonly RedisConnectionManager _connectionManager;

    public CacheRepository(RedisConnectionManager connectionManager, RedisSettings settings)
    {
        _connectionManager = connectionManager;
        _settings = settings;
        var connection = _connectionManager.GetConnection();
        _database = connection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (!value.IsNull)
            return Deserialize<T>(value!);

        return default;
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var expiry = TimeSpan.FromSeconds(_settings.CacheExpirationInSeconds);
        await _database.StringSetAsync(key, Serialize(value), expiry);
    }

    public async Task<bool> Remove(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    private byte[] Serialize<T>(T value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    private T? Deserialize<T>(byte[] bytes)
    {
        var jsonString = Encoding.UTF8.GetString(bytes);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}
