namespace Crawler.Domain.Enrollments.Responses;

public class SearchEnrollmentsResponse
{
    public string CPF { get; set; } = string.Empty;
    public List<string> Enrollments { get; set; } = new List<string>();
}
