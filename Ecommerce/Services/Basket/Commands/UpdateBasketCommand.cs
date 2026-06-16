using Basket.Entities;
using MediatR;

namespace Basket.Commands
{
    public record UpdateBasketCommand() : IRequest<ShoppingCart>
    {
        public string UserName { get; set; }
        public ShoppingCart Items { get; set; }
    }
}
