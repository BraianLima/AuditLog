using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AuditLogContext _context;

        public BaseRepository(AuditLogContext context)
        {
            _context = context;
        }

        public void Detach(TEntity entity)
        {
            var local = _context.Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public void SetOriginalValues(TEntity newEntity, TEntity oldEntity)
        {
            _context.Entry(newEntity).OriginalValues.SetValues(oldEntity);
        }

        public async Task<TEntity> ExecuteTryCatchReturnEntity(Func<Task<TEntity>> func)
        {
            return await Task.Run(async () => 
            { 
                try
                {
                    return await func();
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public async Task<List<TEntity>> ExecuteTryCatchReturnListEntity(Func<Task<List<TEntity>>> func)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    return await func();
                }
                catch (Exception)
                {
                    return null;
                }
            });

        }

    }
}
