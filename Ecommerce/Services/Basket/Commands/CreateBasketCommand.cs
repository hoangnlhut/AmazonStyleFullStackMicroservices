using Basket.DTOs;
using Basket.Responses;
using MediatR;

namespace Basket.Commands
{
    public record CreateBasketCommand(string UserName, List<CreateShoppingCartItemDto> Items) : IRequest<ShoppingCartResponse>
    {
    }
}
