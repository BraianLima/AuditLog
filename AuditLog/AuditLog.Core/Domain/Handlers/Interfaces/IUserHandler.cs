using AuditLog.Core.Domain.DTOs;
using AuditLog.Core.Domain.DTOs.User;
using AuditLog.Core.Domain.Responses;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<Response> GetByIdAsync(int id);
        Task<Response> InsertAsync(UserDTO userDto);
        Task<Response> UpdateAsync(UserDTO userDto);
        Task<Response> DeleteByIdAsync(int id);
        Task<Response> LoginAsync(UserAuthenticationDTO userAuthenticationDto);
    }
}
