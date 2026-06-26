using Discount.Commands;
using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class UpdateDiscountHandle : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandle(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new Dictionary<string, string>();

            //input validation
            if (request.Id <= 0)
                validationErrors["Id"] = "Id must not be empty";
            if (string.IsNullOrWhiteSpace(request.ProductName))
                validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.Description))
                validationErrors["Description"] = "Product Description must not be empty";
            if (request.Amount <= 0)
                validationErrors["Amount"] = "Amount must be greater than zero";

            if (validationErrors.Any())
            {
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            var coupon = request.ToEntity();
            var result = await _discountRepository.UpdateCoupon(coupon);

            if (!result)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Cannot update discount for product {request.ProductName}"));
            }

            // to DTO
            return coupon.ToDto();
        }
    }
}
