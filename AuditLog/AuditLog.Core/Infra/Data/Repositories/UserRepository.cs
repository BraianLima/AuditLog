using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Infra.Data.Context;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AuditLogContext _context;
        public UserRepository(AuditLogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await ExecuteTryCatchReturnEntity(async () =>
            {
                return await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            });
        }

        public async Task<User> InsertAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                user.Id = 0;
            }

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                Detach(user);
                SetOriginalValues(user, await GetByIdAsync(user.Id));

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Users.FindAsync(id);

                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await ExecuteTryCatchReturnEntity(async () =>
            {
                return await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();
            });
        }

    }
}
