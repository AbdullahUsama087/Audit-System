using AuditSystem.Application.Interfaces;
using AuditSystem.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Services.Audit
{

    //Background Service always running in the Background take auditEvents from Queue and Save it into Database
    public class AuditBackgroundService : BackgroundService
    {
        private readonly IAuditEventQueue _queue;
        private readonly IServiceProvider _serviceProvider;

        public AuditBackgroundService(IAuditEventQueue queue, IServiceProvider serviceProvider)
        {
            _queue = queue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) // still works even if the project is running
            {
                try
                {
                    // Get Events from Queue As soon as there is new Audits
                    var auditEvent = await _queue.DequeueAsync(stoppingToken);

                    Console.WriteLine($"Processing Audit Event for UserId={auditEvent.UserId}");


                    // to make new Scope and get DbContext from it and we use it as Background Service dosn't run in HTTP Request
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                    // Add new Audit log into database
                    var log = new AuditLog
                    {
                        UserId = auditEvent.UserId,
                        Action = auditEvent.Action,
                        EntityName = auditEvent.EntityName,
                        EntityId = auditEvent.EntityId,
                        CreatedAt = auditEvent.Timestamp,
                        Metadata = auditEvent.Metadata ?? "{}"
                    };

                    await context.AddAsync(log);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing audit: {ex.Message}");

                    throw;
                }


            }
        }
    }
}
