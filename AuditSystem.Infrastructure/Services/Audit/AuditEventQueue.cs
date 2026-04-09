using AuditSystem.Application.Events;
using AuditSystem.Application.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using static Azure.Core.HttpHeader;

namespace AuditSystem.Infrastructure.Services.Audit
{
    public class AuditEventQueue : IAuditEventQueue
    {
        // Channel Checks on multi threads [thread safe]  // Unbounded : as the row  was unlimited
        private readonly Channel<AuditEvent> _queue = Channel.CreateUnbounded<AuditEvent>();

        // Add Event for the Queue
        public Task EnqueueAsync(AuditEvent auditEvent)
        {
            _queue.Writer.TryWrite(auditEvent); // Add the event on the channel and it's fast as it's Unbounded
            return Task.CompletedTask;
        }

        // Responible for Read Events from Queue Asyncrounsly
        public async Task<AuditEvent> DequeueAsync(CancellationToken cancellationToken) // Cancellation token : Allow to cancel operation if needed
        {
            return await _queue.Reader.ReadAsync(cancellationToken); // wait if the Queue is empty until a new event comes along.
        }

    }
}
