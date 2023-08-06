using Crawler.Domain.Enrollments;
using Crawler.Domain.Enrollments.Requests;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<Result> SeachForEnrollments([FromBody] SearchEnrollmentsRequest request)
        {
            var response = _service.SendEnrollmentSearchRequest(request);
            
            return response.IsSuccess 
                ? Ok(response) 
                : BadRequest(response);
        }
    }
}
