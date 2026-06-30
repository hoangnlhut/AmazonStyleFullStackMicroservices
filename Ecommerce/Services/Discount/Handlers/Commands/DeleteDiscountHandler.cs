using Discount.Commands;
using Discount.Extensions;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers.Commands
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountHandler(IDiscountRepository discountRepository)
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
            return result;
        }
    }
}
