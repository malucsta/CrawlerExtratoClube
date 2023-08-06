using Crawler.Infra.Components.Interfaces.Search;
using Crawler.Infra.Elasticsearch.Connection;
using Crawler.Infra.Elasticsearch.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Infra.Elasticsearch;

public static class ExtensionMethods
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new ElasticSettings();
        configuration.GetSection("ElasticSearch").Bind(settings);
        services.AddSingleton(settings);

        services.AddSingleton(provider =>
        {
            return new ConnectionManager(settings.Url, settings.DefaultIndex);
        });

        services.AddTransient(typeof(ISearchRepository<>), typeof(ElasticsearchRepository<>));

        return services;
    }
}
