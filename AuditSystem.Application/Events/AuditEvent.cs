using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Events
{
    public class AuditEvent
    {
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Metadata { get; set; } = "{}";
    }
}
