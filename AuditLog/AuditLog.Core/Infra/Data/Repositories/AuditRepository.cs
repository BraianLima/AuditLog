using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Infra.Data.Context;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        private readonly AuditLogContext _context;
        public AuditRepository(AuditLogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Audit>> GetListAuditsByUserIdAsync(int userId)
        {
            return await ExecuteTryCatchReturnListEntity(async () =>
            {
                return await _context.Audits
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            });
        }

        public async Task<Audit> InsertAsync(Audit audit)
        {
            try
            {
                await _context.Audits.AddAsync(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                audit.Id = 0;
            }

            return audit;
        }

    }
}
