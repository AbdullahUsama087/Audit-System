using AuditSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
