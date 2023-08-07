namespace Crawler.Domain.Enrollments;

public class Enrollment
{
    public string CPF { get; set; } = string.Empty;
    public List<string> Enrollments { get; set; } = new List<string>();
    public DateTime CreatedAt { get; set; }
}
