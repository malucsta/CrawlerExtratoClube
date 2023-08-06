using Nest;

namespace Crawler.Infra.Elasticsearch.Connection;

public class ConnectionManager
{
    private readonly ElasticClient _elasticClient;

    public ConnectionManager(string elasticsearchUrl, string defaultIndex)
    {
        var settings = new ConnectionSettings(new Uri(elasticsearchUrl)).DefaultIndex(defaultIndex);
        _elasticClient = new ElasticClient(settings);
    }

    public ElasticClient GetClient()
    {
        return _elasticClient;
    }
}
