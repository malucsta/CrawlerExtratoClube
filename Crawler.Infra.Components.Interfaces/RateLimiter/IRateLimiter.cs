namespace Crawler.Infra.Components.Interfaces.RateLimiter;

public interface IRateLimiter
{
    Task<bool> IsLimited(string key);
}
