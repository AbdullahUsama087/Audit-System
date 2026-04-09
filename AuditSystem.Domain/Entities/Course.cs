using AuditSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
    }
}
