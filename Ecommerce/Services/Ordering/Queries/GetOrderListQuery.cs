using MediatR;
using Ordering.DTOs;

namespace Ordering.Queries
{
    public record GetOrderListQuery(string UserName) : IRequest<List<OrderDto>>
    {
    }
}
