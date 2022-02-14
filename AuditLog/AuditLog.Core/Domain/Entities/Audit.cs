using System;

namespace AuditLog.Core.Domain.Entities
{
    public class Audit : BaseEntity
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string OldObject { get; set; }
        public string NewObject { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
