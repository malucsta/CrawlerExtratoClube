using Crawler.Infra.Components.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Crawler.Infra.RabbitMq;
using Crawler.Infra.Elasticsearch;

namespace Crawler.Infra.Components;

public static class ExtensionMethods
{
    public static IServiceCollection AddInfraComponents(this IServiceCollection services, IConfiguration configuration)
    {
        // CONNECTIONS
        services.AddRedisConnection(configuration);

        // COMPONENTS 
        services.AddCache();
        services.AddRateLimiter();
        services.AddRabbitMq(configuration);
        services.AddElasticSearch(configuration);

        return services;
    }

    private static IServiceCollection AddRedisConnection(this IServiceCollection services, IConfiguration configuration)
    {
        Redis.ExtensionMethods.ConfigureRedisConnection(services, configuration);
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services)
    {
        Redis.ExtensionMethods.ConfigureRedisCache(services);
        return services;
    }

    private static IServiceCollection AddRateLimiter(this IServiceCollection services)
    {
        Redis.ExtensionMethods.ConfigureRedisRateLimiter(services);
        return services;
    }

    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RateLimitingMiddleware>();
    }


}
