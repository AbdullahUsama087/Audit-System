using AuditSystem.Application.Interfaces;
using AuditSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Queries
{
    public class GetEnrollmentsHandler
    {
        private readonly IApplicationDbContext _context;

        public GetEnrollmentsHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        // Handle Select Query by EF
        // This is obvious example on CQRS : Select and retrieve data without any Create,Update or Delete 
        public IQueryable<Enrollment> Handle(GetEnrollmentsQuery query)
        {
            var enrollments = _context.Enrollments;

            if (query.UserId.HasValue)
                enrollments = enrollments.Where(e => e.UserId == query.UserId.Value);

            return enrollments;
        }

    }
}
