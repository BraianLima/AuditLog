using Microsoft.OpenApi.Models;

namespace AuditLog.Core.Domain.ValueObjects
{
    public class ApiKeyScheme : OpenApiSecurityScheme
    {
        public new string Description { get; set; }
        public new string Name { get; set; }
        public new string In { get; set; }
        public new string Type { get; set; }
    }
}
