using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace AuditLog.Core.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditHandler _auditHandler;

        public AuditService(IAuditHandler auditHandler)
        {
            _auditHandler = auditHandler;
        }

        public async Task<Response> GetListAuditsByUserIdAsync(int userId) 
            => await _auditHandler.GetListAuditsByUserIdAsync(userId);

        public async Task<Response> InsertAsync(AuditDTO auditDto) 
            => await _auditHandler.InsertAsync(auditDto);
    }
}
