using Crawler.Domain.Enrollments;
using Crawler.Domain.Enrollments.Requests;
using Crawler.Domain.Enrollments.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Result<SearchEnrollmentsResponse>>> SeachForEnrollments([FromBody] SearchEnrollmentsRequest request)
        {
            var response = await _service.ProcessEnrollmentSearchRequest(request);

            return response.IsSuccess
                ? response.Value is not null ? Ok(response) : Accepted()
                : BadRequest(response);
        }
    }
}
