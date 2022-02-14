using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Responses;
using System.Threading.Tasks;

namespace AuditLog.Core.Services.Interfaces
{
    public interface IAuditService
    {
        Task<Response> GetListAuditsByUserIdAsync(int userId);
        Task<Response> InsertAsync(AuditDTO auditDto);
    }
}
