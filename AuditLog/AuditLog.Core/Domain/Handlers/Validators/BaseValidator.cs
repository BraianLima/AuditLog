using AuditLog.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers.Validators
{
    public abstract class BaseValidator
    {
        public bool ContainId(int? id)
        {
            return GenericMethods.ContainId(id);
        }
    }
}
