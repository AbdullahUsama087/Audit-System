using AuditSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
