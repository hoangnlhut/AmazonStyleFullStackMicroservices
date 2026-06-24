using Discount.Commands;
using Discount.Extensions;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class DeleteDiscountHandle : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountHandle(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            //Validate the input
            if (string.IsNullOrWhiteSpace(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "Product name must not be empty" }
                };
                throw GrpcErrorHelper.CreateRpcValidationException(validationErrors);
            }

            var result = await _discountRepository.DeleteCoupon(request.productName);

            if (!result)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Cannot delete discount for product {request.productName}"));
            }

            return result;
        }
    }
}
