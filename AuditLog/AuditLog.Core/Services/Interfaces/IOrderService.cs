using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.Responses;
using System.Threading.Tasks;

namespace AuditLog.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Response> GetByIdAsync(int id);
        Task<Response> InsertAsync(OrderDTO orderDto);
        Task<Response> UpdateAsync(OrderDTO orderDto);
        Task<Response> DeleteByIdAsync(int id);
    }
}
