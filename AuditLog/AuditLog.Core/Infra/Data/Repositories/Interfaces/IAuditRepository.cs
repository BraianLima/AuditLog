using AuditLog.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task<Audit> InsertAsync(Audit audit);
        Task<List<Audit>> GetListAuditsByUserIdAsync(int userId);
    }
}
