using Crawler.Domain.Enrollments.Requests;
using Crawler.Domain.Enrollments.Responses;
using FluentResults;

namespace Crawler.Domain.Enrollments;

public interface IEnrollmentService
{
    Task<Result<SearchEnrollmentsResponse>> ProcessEnrollmentSearchRequest(SearchEnrollmentsRequest request);
}
