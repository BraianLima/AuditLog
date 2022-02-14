using System;
using System.Collections.Generic;

namespace AuditLog.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
