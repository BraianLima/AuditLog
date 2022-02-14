using System;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers
{
    public abstract class BaseHandlers
    {
        public bool ExecuteValidate(Func<bool> func)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<object> TaskRunAsync(Func<object> func)
        {
            return await Task.Run(() => func());
        }
    }
}
