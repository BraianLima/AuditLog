using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.Responses;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers.Interfaces
{
    public interface IOrderHandler
    {
        Task<Response> GetByIdAsync(int id);
        Task<Response> InsertAsync(OrderDTO orderDto);
        Task<Response> UpdateAsync(OrderDTO orderDto);
        Task<Response> DeleteByIdAsync(int id);
    }
}
