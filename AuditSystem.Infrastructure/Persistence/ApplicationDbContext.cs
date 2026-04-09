using AuditSystem.Application.Interfaces;
using AuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Course> Courses => Set<Course>();

        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        // Query
        IQueryable<User> IApplicationDbContext.Users => Users.AsQueryable();
        IQueryable<Course> IApplicationDbContext.Courses => Courses.AsQueryable();
        IQueryable<Enrollment> IApplicationDbContext.Enrollments => Enrollments.AsQueryable();


        // Commands
        public new async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            await Set<T>().AddAsync(entity, cancellationToken);
        }

        public async new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
