using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Utils;

namespace AuditLog.Core.Domain.Handlers.Validators
{
    public class OrderValidator : BaseValidator
    {
        public bool OrderIsValid(Order order)
        {
            if (GenericMethods.IsNull(order))
                return false;

            if (!GenericMethods.ContainId(order.UserId))
                return false;

            return true;
        }

        public bool OrderDtoIsValid(OrderDTO orderDto)
        {
            if (GenericMethods.IsNull(orderDto))
                return false;

            return true;
        }

        public bool InsertIsValid(Order order)
        {
            return GenericMethods.InsertIsValid(order);
        }

    }
}
