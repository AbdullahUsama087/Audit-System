using AuditSystem.Application.Features.Enrollments.Commands.Enroll_Course;
using AuditSystem.Application.Features.Enrollments.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuditSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollCourseHandler _handler;
        private readonly GetEnrollmentsHandler _getEnrollmentsHandler;

        public EnrollmentController(EnrollCourseHandler handler, GetEnrollmentsHandler getEnrollmentsHandler)
        {
            _handler = handler;
            _getEnrollmentsHandler = getEnrollmentsHandler;
        }


        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollCourseCommand courseCommand)
        {
            var enrollmentId = await _handler.Handle(courseCommand);

            return Ok(new { Message = "Enrolled Successfully", EnrollmentId = enrollmentId });
        }


        [HttpGet]
        public async Task<IActionResult> GetEnrollments([FromQuery] int? userId)
        {
            var query = new GetEnrollmentsQuery
            {
                UserId = userId
            };

            var enrollmentQuery = _getEnrollmentsHandler.Handle(query);

            var enrollments = await enrollmentQuery.ToListAsync();

            return Ok(enrollments);
        }

    }
}
