using System;

namespace AuditLog.Core.Domain.DTOs.Audit
{
    public class AuditDTO
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string OldObject { get; set; }
        public string NewObject { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }

        public object OldObjectTypeObject { get; set; }
        public object NewObjectTypeObject { get; set; }
        public bool DifferentObjects { get; set; }
        public string UserName { get; set; }
    }
}
