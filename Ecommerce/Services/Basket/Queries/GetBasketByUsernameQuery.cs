using Basket.Entities;
using MediatR;

namespace Basket.Queries
{
    public record GetBasketByUsernameQuery(string UserName) : IRequest<ShoppingCart>
    {
    }
}
