using Basket.Entities;
using Basket.Mappers;
using Basket.Queries;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers.Query
{
    public class GetBasketByUsernameHandler : IRequestHandler<GetBasketByUsernameQuery, BasketResponse>
    {
        private readonly IBasketRepository _repository;
        public GetBasketByUsernameHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<BasketResponse> Handle(GetBasketByUsernameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _repository.GetBasket(request.UserName);
            if (shoppingCart == null) 
            {
                return new BasketResponse(request.UserName);
            }

            return shoppingCart.ToResponse();
        }
    {
    }
}
