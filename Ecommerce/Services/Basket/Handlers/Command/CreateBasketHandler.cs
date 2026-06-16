using Basket.Commands;
using Basket.Entities;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.Handlers.Command
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateBasketHandler(IBasketRepository basketRepository) 
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var entity = request.CommandToEntity();

            var shoppingCart = await _basketRepository.UpsertBasket(entity);

            if (shoppingCart is null)
            {
                return new ShoppingCartResponse(request.UserName);
            }

            return shoppingCart.ToResponse();
        }
    }
}
