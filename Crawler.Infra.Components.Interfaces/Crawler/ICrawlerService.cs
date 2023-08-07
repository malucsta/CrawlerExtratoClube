namespace Crawler.Infra.Components.Interfaces.Crawler;

public interface ICrawlerService
{
    IEnumerable<string> CrawlExtratoClubeEnrollments(string user, string password, string cpf);
}
