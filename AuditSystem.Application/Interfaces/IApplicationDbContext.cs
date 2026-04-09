using AuditSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        // Commands
        Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Query
        IQueryable<Enrollment> Enrollments { get; }
        IQueryable<User> Users { get; }
        IQueryable<Course> Courses { get; }
    }
}
