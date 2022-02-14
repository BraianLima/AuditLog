using AuditLog.Core.Domain.DTOs;
using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace AuditLog.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHandler _orderHandler;

        public OrderService(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        public async Task<Response> GetByIdAsync(int id) => await _orderHandler.GetByIdAsync(id);

        public async Task<Response> InsertAsync(OrderDTO orderDto) => await _orderHandler.InsertAsync(orderDto);

        public async Task<Response> UpdateAsync(OrderDTO orderDto) => await _orderHandler.UpdateAsync(orderDto);

        public async Task<Response> DeleteByIdAsync(int id) => await _orderHandler.DeleteByIdAsync(id);


    }
}
