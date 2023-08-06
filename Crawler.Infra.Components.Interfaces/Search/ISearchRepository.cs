namespace Crawler.Infra.Components.Interfaces.Search;

public delegate IEnumerable<IField> InferSearchFields<T>(T item);

public interface ISearchRepository<T> where T : class
{
    Task<bool> IndexDocumentAsync(T document);
    Task<bool> DeleteDocumentAsync(string id);
    Task<IEnumerable<T>> SearchAsync(string query, InferSearchFields<T> searchFieldsSelector);

    Task<IEnumerable<T>> SearchAsync(string query);

    Task<T> Search(string id);
}
