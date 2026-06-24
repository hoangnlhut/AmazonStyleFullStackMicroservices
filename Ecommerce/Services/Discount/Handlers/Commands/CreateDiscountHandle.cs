using Discount.Commands;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class CreateDiscountHandle : IRequestHandler<CreateDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;
        public CreateDiscountHandle(IDiscountRepository discountRepository) 
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = new Dictionary<string, string>();

            //input validation
            if (string.IsNullOrWhiteSpace(request.coupon.ProductName))
                validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.coupon.Description))
                validationErrors["Description"] = "Product Description must not be empty";
            if (request.coupon.Amount <= 0)
                validationErrors["Amount"] = "Amount must be greater than zero";

            if(validationErrors.Any()) 
            {
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            var coupon = request.coupon.ToEntity();
            var result = await _discountRepository.CreateCoupon(coupon);
            if (!result)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Cannot create discount for product {request.coupon.ProductName}"));
            }

            return result;
        }
    }
}
