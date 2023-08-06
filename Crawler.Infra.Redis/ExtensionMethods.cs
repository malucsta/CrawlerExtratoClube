using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Components.Interfaces.RateLimiter;
using Crawler.Infra.Redis.Connection;
using Crawler.Infra.Redis.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Infra.Redis;

public static class ExtensionMethods
{
    public static IServiceCollection ConfigureRedisConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = new RedisSettings();
        configuration.GetSection("Redis").Bind(redisSettings);
        services.AddSingleton(redisSettings);

        services.AddSingleton<RedisConnectionManager>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services)
    {
        services.AddScoped<ICacheRepository, CacheRepository>();
        
        return services;
    }

    public static IServiceCollection ConfigureRedisRateLimiter(this IServiceCollection services)
    {
        services.AddSingleton<IRateLimiter, RedisRateLimiter>();
        return services;
    }
}
