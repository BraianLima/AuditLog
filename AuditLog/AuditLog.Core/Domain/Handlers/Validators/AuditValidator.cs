using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Utils;
using System.Collections.Generic;

namespace AuditLog.Core.Domain.Handlers.Validators
{
    public class AuditValidator
    {
        public bool ListAuditsIsValid(List<Audit> listAudits)
        {
            if (GenericMethods.IsNull(listAudits))
                return false;

            return true;
        }

        public bool AuditDtoIsValid(AuditDTO auditDto)
        {
            if (GenericMethods.IsNull(auditDto))
                return false;

            return true;
        }

        public bool InsertIsValid(Audit audit)
        {
            return GenericMethods.InsertIsValid(audit);
        }
    }
}
