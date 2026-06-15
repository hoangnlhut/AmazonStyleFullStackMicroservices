using Basket.Entities;
using Basket.Queries;
using Basket.Repositories;
using MediatR;

namespace Basket.Handlers
{
    public class GetBasketByUsernameHandler : IRequestHandler<GetBasketByUsernameQuery, ShoppingCart>
    {
        private readonly IBasketRepository _repository;
        public GetBasketByUsernameHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<ShoppingCart> Handle(GetBasketByUsernameQuery request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasket(request.UserName);
            return basket;
        }
    {
    }
}
