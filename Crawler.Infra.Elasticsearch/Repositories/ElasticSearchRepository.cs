using Crawler.Infra.Components.Interfaces.Search;
using Crawler.Infra.Elasticsearch.Connection;
using Nest;

namespace Crawler.Infra.Elasticsearch.Repositories;

public class ElasticsearchRepository<T> : ISearchRepository<T> where T : class
{
    private readonly ElasticClient _client;

    public ElasticsearchRepository(ConnectionManager _manager)
    {
        _client = _manager.GetClient();
    }

    public async Task<bool> IndexDocumentAsync(T document)
    {
        var response = await _client.IndexDocumentAsync(document);
        return response.IsValid;
    }

    public async Task<bool> DeleteDocumentAsync(string id)
    {
        var response = await _client.DeleteAsync<T>(id);
        return response.IsValid;
    }

    public async Task<T> Search(string id)
    {
        var result = await _client.GetAsync<T>(id);
        return result.Source;
    }

    public async Task<IEnumerable<T>> SearchAsync(string query, InferSearchFields<T> searchFieldsSelector)
    {
        var fields = searchFieldsSelector.Invoke(default(T));
        
        var response = await _client.SearchAsync<T>(s => s
            .Query(q => q
                .MultiMatch(m => m
                    .Fields((Fields)fields)
                    .Query(query)
                )
            )
        );

        return response.Documents;
    }

    public async Task<IEnumerable<T>> SearchAsync(string query)
    {
        var response = await _client.SearchAsync<T>(s => s
            .Query(q => q
                .Match(m => m
                    .Query(query)
                )
            )
        );

        return response.Documents;
    }
}
