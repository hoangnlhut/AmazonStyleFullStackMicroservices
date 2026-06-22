using Discount.DTOs;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Queries
{
    public class GetDiscountByProductNameHandle : IRequestHandler<GetDiscountByProductNameQuery, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountByProductNameHandle(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetCouponByProductName(request.productName);
            if (coupon == null || coupon.Id == 0) 
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for the product name {request.productName}"));
            }
            return coupon.ToDto();
        }
    }
}
