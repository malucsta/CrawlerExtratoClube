namespace Crawler.Domain.Enrollments.Requests;

public class SearchEnrollmentsRequest
{
    public string CPF { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
