using Crawler.Infra.Components.Interfaces.Cache;
using Crawler.Infra.Redis.Connection;
using Crawler.Infra.Redis.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Infra.Redis;

public static class ExtensionMethods
{
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = new RedisSettings();
        configuration.GetSection("Redis").Bind(redisSettings);
        services.AddSingleton(redisSettings);

        services.AddSingleton<RedisConnectionManager>();
        services.AddScoped<ICacheRepository, CacheRepository>();
        return services;
    }
}
