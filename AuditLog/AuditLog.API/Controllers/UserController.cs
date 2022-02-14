using AuditLog.Core.Domain.DTOs.User;
using AuditLog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuditLog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationDTO userAuthenticationDto)
        {
            var response = await _userService.LoginAsync(userAuthenticationDto);
            return ReturnActionResult(response);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            return ReturnActionResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertAsync([FromBody] UserDTO userDto)
        {
            var response = await _userService.InsertAsync(userDto);
            return ReturnActionResult(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDTO userDto)
        {
            var response = await _userService.UpdateAsync(userDto);
            return ReturnActionResult(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var response = await _userService.DeleteByIdAsync(id);
            return ReturnActionResult(response);
        }

    }
}
