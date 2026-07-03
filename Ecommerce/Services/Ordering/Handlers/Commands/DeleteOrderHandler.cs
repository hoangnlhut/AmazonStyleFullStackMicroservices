using MediatR;
using Ordering.Commands;
using Ordering.Entities;
using Ordering.Exceptions;
using Ordering.Repositories;

namespace Ordering.Handlers.Commands
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderHandler> _logger;

        public DeleteOrderHandler(IOrderRepository orderRepository, ILogger<DeleteOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                _logger.LogWarning($"Order with id {request.Id} not found.");
                throw new OrderException(nameof(Order), request.Id, "is not found");
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with id {request.Id} deleted successfully.");
            return Unit.Value;
        }
    }
}
