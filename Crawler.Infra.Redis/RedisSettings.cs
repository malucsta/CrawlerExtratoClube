namespace Crawler.Infra.Redis;

public class RedisSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int RateLimitExpirationInSeconds { get; set; } = 300;
    public int RateLimitMaxRequest { get; set; } = 10;
    public int CacheExpirationInSeconds { get; set; } = 300;
    public int MaxRetryConnectionAttempts { get; set; } = 5;
    public double InitialRetryDelayInSeconds { get; set; } = 1;
}
