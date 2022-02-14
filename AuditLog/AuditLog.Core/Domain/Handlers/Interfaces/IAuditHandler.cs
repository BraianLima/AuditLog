using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Responses;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers.Interfaces
{
    public interface IAuditHandler
    {
        Task<Response> GetListAuditsByUserIdAsync(int userId);
        Task<Response> InsertAsync(AuditDTO auditDto);
    }
}
