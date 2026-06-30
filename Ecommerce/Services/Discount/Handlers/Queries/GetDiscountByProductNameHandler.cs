using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Discount.Handlers.Queries
{
    public class GetDiscountByProductNameHandler : IRequestHandler<GetDiscountByProductNameQuery, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountByProductNameHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
        {
            //Validate the input
            if(string.IsNullOrWhiteSpace(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "Product name must not be empty" }
                };
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            //fetch from repo
            var coupon = await _discountRepository.GetCouponByProductName(request.productName);
            if (coupon == null || coupon.Id == 0) 
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for the product name {request.productName}"));
            }

            //mapping
            return coupon.ToDto();
        }
    }
}
