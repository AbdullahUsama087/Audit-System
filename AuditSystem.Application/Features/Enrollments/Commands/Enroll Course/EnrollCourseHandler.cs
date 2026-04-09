using AuditSystem.Application.Events;
using AuditSystem.Application.Interfaces;
using AuditSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Commands.Enroll_Course
{
    public class EnrollCourseHandler
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuditEventQueue _auditQueue;

        public EnrollCourseHandler(IApplicationDbContext context, IAuditEventQueue auditQueue)
        {
            _context = context;
            _auditQueue = auditQueue;
        }

        // Handling Operation for Enrolling New Course to User
        public async Task<int> Handle(EnrollCourseCommand command)
        {
            // Validation
            if (command.UserId <= 0 || command.CourseId <= 0)
                throw new Exception("Invalid Data");

            // Create Enrollment and add it into Database
            var enrollment = new Enrollment
            {
                UserId = command.UserId,
                CourseId = command.CourseId,
                EnrolledAt = DateTime.UtcNow
            };

            await _context.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            // Publish Event
            // record that the user has enrolled in a course, without affecting the main performance (asyncrounsly).
            // This is obvious example on CQRS : one command executed, change DB state and Publish Audit without any retrieveing data
            await _auditQueue.EnqueueAsync(new AuditEvent
            {
                UserId = command.UserId,
                Action = "EnrollCourse",
                EntityName = "Enrollment",
                EntityId = enrollment.Id,
                Timestamp = DateTime.UtcNow,
                Metadata = $"{{ \"CourseId\": {command.CourseId} }}"
            });

            return enrollment.Id;
        }
    }
}
