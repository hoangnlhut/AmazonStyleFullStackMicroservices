using Discount.Commands;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class UpdateDiscountHandle : IRequestHandler<UpdateDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandle(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new Dictionary<string, string>();

            //input validation
            if (request.couponInput.Id <= 0)
                validationErrors["Id"] = "Id must not be empty";
            if (string.IsNullOrWhiteSpace(request.couponInput.ProductName))
                validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.couponInput.Description))
                validationErrors["Description"] = "Product Description must not be empty";
            if (request.couponInput.Amount <= 0)
                validationErrors["Amount"] = "Amount must be greater than zero";

            if (validationErrors.Any())
            {
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            var coupon = request.couponInput.ToEntity();
            var result = await _discountRepository.UpdateCoupon(coupon);

            if (!result)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Cannot update discount for product {request.couponInput.ProductName}"));
            }

            return result;
        }
    }
}
