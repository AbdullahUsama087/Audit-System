using AuditSystem.Application.Interfaces;
using AuditSystem.Infrastructure.Persistence;
using AuditSystem.Infrastructure.Services.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddScoped<IApplicationDbContext>(provider =>
                    provider.GetRequiredService<ApplicationDbContext>());


            // Audit Queue + Background Service
            services.AddSingleton<IAuditEventQueue, AuditEventQueue>();
            services.AddHostedService<AuditBackgroundService>();

            return services;
        }
    }
}
