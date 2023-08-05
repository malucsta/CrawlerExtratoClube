namespace Crawler.Infra.Components.Interfaces.Cache;

public interface ICacheRepository
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task<bool> Remove(string key);
}
