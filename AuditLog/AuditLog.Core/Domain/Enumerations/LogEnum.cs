using System.ComponentModel.DataAnnotations;

namespace AuditLog.Core.Domain.Enumerations
{
    public enum LogEnum
    {
        [Display(Name = "INSERT")]
        INSERT = 1,
        [Display(Name = "UPDATE")]
        UPDATE = 2,
        [Display(Name = "DELETE")]
        DELETE = 3,
        [Display(Name = "LOGIN")]
        LOGIN = 4
    }
}
