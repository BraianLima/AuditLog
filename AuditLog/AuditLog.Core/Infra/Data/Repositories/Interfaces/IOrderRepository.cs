using AuditLog.Core.Domain.Entities;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<Order> InsertAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteByIdAsync(int id);
    }
}
