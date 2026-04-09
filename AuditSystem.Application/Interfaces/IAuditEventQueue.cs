using AuditSystem.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Interfaces
{
    public interface IAuditEventQueue
    {
        Task EnqueueAsync(AuditEvent auditEvent);
        Task<AuditEvent> DequeueAsync(CancellationToken cancellationToken);
    }
}
