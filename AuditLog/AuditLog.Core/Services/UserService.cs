using AuditLog.Core.Domain.DTOs.User;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace AuditLog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserHandler _userHandler;

        public UserService(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        public async Task<Response> GetByIdAsync(int id) => await _userHandler.GetByIdAsync(id);

        public async Task<Response> InsertAsync(UserDTO userDto) => await _userHandler.InsertAsync(userDto);

        public async Task<Response> UpdateAsync(UserDTO userDto) => await _userHandler.UpdateAsync(userDto);

        public async Task<Response> DeleteByIdAsync(int id) => await _userHandler.DeleteByIdAsync(id);

        public async Task<Response> LoginAsync(UserAuthenticationDTO userAuthenticationDto)
            => await _userHandler.LoginAsync(userAuthenticationDto);
    }
}
