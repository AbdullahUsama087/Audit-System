using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Commands.Enroll_Course
{
    public class EnrollCourseCommand
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
