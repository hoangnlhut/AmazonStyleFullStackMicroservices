using Basket.Responses;
using MediatR;

namespace Basket.Queries
{
    public record GetBasketByUsernameQuery(string UserName) : IRequest<ShoppingCartResponse>
    {
    }
}
