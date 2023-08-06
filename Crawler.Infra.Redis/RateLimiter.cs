using Crawler.Infra.Components.Interfaces.RateLimiter;
using Crawler.Infra.Redis.Connection;
using StackExchange.Redis;

namespace Crawler.Infra.Redis;

public class RedisRateLimiter : IRateLimiter
{
    private readonly IDatabase _database;
    private readonly RedisSettings _settings;
    private readonly TimeSpan _expirationTime;

    public RedisRateLimiter(RedisSettings settings)
    {
        _settings = settings;

        var connection = new RedisConnectionManager(_settings).GetConnection();
        _database = connection.GetDatabase();

        _expirationTime = TimeSpan.FromSeconds(_settings.RateLimitExpirationInSeconds);
    }

    public async Task<bool> IsLimited(string key)
    {
        var currentCount = await _database.StringIncrementAsync(key);

        if (currentCount == 1)
            _database.KeyExpire(key, _expirationTime);

        return currentCount <= _settings.RateLimitMaxRequest;
    }
}
