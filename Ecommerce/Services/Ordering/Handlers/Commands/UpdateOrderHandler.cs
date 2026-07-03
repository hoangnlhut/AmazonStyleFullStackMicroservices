using MediatR;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Entities;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers.Commands
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderHandler> _logger;

        public UpdateOrderHandler(IOrderRepository orderRepository, ILogger<UpdateOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderExist = await _orderRepository.GetByIdAsync(request.Id);
            if (orderExist == null)
            {
                _logger.LogWarning($"Order with id {request.Id} not found.");
                throw new ArgumentException($"Order with id {request.Id} not found.");
            }

            orderExist.MapUpdate(request);

            await _orderRepository.UpdateAsync(orderExist);
            _logger.LogInformation($"Order with id {request.Id} updated successfully.");

            return Unit.Value;
        }
    }
}
