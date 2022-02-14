using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuditLog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _orderService.GetByIdAsync(id);
            return ReturnActionResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertAsync([FromBody] OrderDTO orderDto)
        {
            var response = await _orderService.InsertAsync(orderDto);
            return ReturnActionResult(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderDTO orderDto)
        {
            var response = await _orderService.UpdateAsync(orderDto);
            return ReturnActionResult(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var response = await _orderService.DeleteByIdAsync(id);
            return ReturnActionResult(response);
        }

    }
}
