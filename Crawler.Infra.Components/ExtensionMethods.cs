using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Infra.Components;

public static class ExtensionMethods
{
    public static IServiceCollection AddInfraComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCache(configuration);
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        Redis.ExtensionMethods.ConfigureRedisCache(services, configuration);
        return services;
    }
}
