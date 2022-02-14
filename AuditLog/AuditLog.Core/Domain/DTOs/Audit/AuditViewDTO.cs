using System;

namespace AuditLog.Core.Domain.DTOs.Audit
{
    public class AuditViewDTO
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string OldObject { get; set; }
        public string NewObject { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }   
    }
}
