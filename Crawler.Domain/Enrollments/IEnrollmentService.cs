using Crawler.Domain.Enrollments.Requests;
using FluentResults;

namespace Crawler.Domain.Enrollments;

public interface IEnrollmentService
{
    Result SendEnrollmentSearchRequest(SearchEnrollmentsRequest request);
}
