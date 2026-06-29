using Basket.Commands;
using Basket.Entities;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.Handlers.Command
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, BasketResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateBasketHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcServic) 
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcServic;
        }
        public async Task<BasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            //apply discount to each item in the basket
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                if (coupon != null || coupon.Id > 0) { 
                    item.Price -= coupon.Amount;
                }
            }

            var entity = request.CommandToEntity();

            var shoppingCart = await _basketRepository.UpsertBasket(entity);

            if (shoppingCart is null)
            {
                return new BasketResponse(request.UserName);
            }

            return shoppingCart.ToResponse();
        }
    }
}
