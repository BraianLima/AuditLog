using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Global;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Handlers.Validators;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Domain.Utils;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers
{
    public class OrderHandler : BaseHandlers, IOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ObjectFactories _objectFactories;
        private readonly OrderValidator _orderValidator;

        public OrderHandler(IOrderRepository orderRepository,
            IMapper mapper,
            ObjectFactories objectFactories,
            OrderValidator orderValidator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _objectFactories = objectFactories;
            _orderValidator = orderValidator;
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            var isValid = ExecuteValidate(() => _orderValidator.OrderIsValid(order));

            return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, EntityToOrderDto(order));
        }

        public async Task<Response> InsertAsync(OrderDTO orderDto)
        {
            var isValid = ExecuteValidate(() => _orderValidator.OrderDtoIsValid(orderDto));
            if (isValid)
            {
                var order = await _orderRepository.InsertAsync(OrderDtoToEntity(orderDto));
                var insertIsValid = ExecuteValidate(() => _orderValidator.InsertIsValid(order));

                return _objectFactories.ReturnResponseToService(insertIsValid, GlobalVariables.IS_INSERT, EntityToOrderDto(order));
            }

            return (Response)await TaskRunAsync(() =>
                _objectFactories.ReturnResponseToService(isValid, GlobalVariables.IS_INSERT, orderDto));
        }

        public async Task<Response> UpdateAsync(OrderDTO orderDto)
        {
            var containId = _orderValidator.ContainId(orderDto.Id);

            if (containId)
            {
                var isValid = await _orderRepository.UpdateAsync(OrderDtoToEntity(orderDto));
                return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, orderDto);
            }

            return _objectFactories.ReturnResponseToService(containId, GlobalVariables.INSERT_FALSE, orderDto);
        }

        public async Task<Response> DeleteByIdAsync(int id)
        {
            var isValid = await _orderRepository.DeleteByIdAsync(id);
            return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, id);
        }


        #region [ Map Order ]

        private OrderDTO EntityToOrderDto(Order user)
        {
            return _mapper.Map<OrderDTO>(user);
        }

        private Order OrderDtoToEntity(OrderDTO orderDto)
        {
            return _mapper.Map<Order>(orderDto);
        }

        #endregion
    }
}
