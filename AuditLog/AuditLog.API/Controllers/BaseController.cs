using AuditLog.Core.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AuditLog.API.Controllers
{
    public class BaseController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ReturnActionResult(Response response)
        {
            return StatusCode((int)response.StatusCode, response); 
        }


    }
}
