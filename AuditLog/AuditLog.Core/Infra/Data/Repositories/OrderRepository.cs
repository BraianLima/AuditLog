using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Infra.Data.Context;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly AuditLogContext _context;
        public OrderRepository(AuditLogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await ExecuteTryCatchReturnEntity(async () =>
            {
                return await _context.Orders
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            });
        }

        public async Task<Order> InsertAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                order.Id = 0;
            }

            return order;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {
                Detach(order);
                SetOriginalValues(order, await GetByIdAsync(order.Id));

                _context.Orders.Update(order);
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
                var entity = await _context.Orders.FindAsync(id);

                _context.Orders.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
