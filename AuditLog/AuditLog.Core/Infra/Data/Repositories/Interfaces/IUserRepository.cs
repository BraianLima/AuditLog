using AuditLog.Core.Domain.Entities;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> InsertAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
    }
}
