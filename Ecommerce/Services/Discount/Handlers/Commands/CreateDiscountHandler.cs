using Discount.Commands;
using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;
        public CreateDiscountHandler(IDiscountRepository discountRepository) 
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new Dictionary<string, string>();

            //input validation
            if (string.IsNullOrWhiteSpace(request.ProductName))
                validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.Description))
                validationErrors["Description"] = "Product Description must not be empty";
            if (request.Amount <= 0)
                validationErrors["Amount"] = "Amount must be greater than zero";

            if(validationErrors.Any()) 
            {
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            var coupon = request.ToEntity();
            var result = await _discountRepository.CreateCoupon(coupon);
            if (!result)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Cannot create discount for product {request.ProductName}"));
            }

            //Return DTO
            return coupon.ToDto();
        }
    }
}
