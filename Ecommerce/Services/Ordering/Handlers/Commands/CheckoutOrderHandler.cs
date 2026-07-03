using MediatR;
using Ordering.Commands;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers.Commands
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckoutOrderHandler> _logger;

        public CheckoutOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = request.CheckoutToEntity();
            var generateOrder = await _orderRepository.AddAsync(entity);
            _logger.LogInformation($"Order {generateOrder.Id} is successfully created.");
            return generateOrder.Id;
        }
    }
}
