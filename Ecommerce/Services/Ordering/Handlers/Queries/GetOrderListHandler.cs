using MediatR;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;
using Ordering.Repositories;

namespace Ordering.Handlers.Queries
{
    public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderListHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.UserName))
                {
                throw new ArgumentException("UserName cannot be null or empty.", nameof(request.UserName));
            }

            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);

            return orderList.ToDtos().ToList();
        }
    }
}
