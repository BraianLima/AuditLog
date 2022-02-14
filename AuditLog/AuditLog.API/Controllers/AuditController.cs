using AuditLog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuditLog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;
        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> GetListAuditsByUserIdAsync(int userId)
        {
            var response = await _auditService.GetListAuditsByUserIdAsync(userId);
            return ReturnActionResult(response);
        }
    }
}
