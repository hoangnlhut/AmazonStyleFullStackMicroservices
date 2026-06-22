using Discount.Commands;
using Discount.Repositories;
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
            var result = await _discountRepository.DeleteCoupon(request.productName);
            return result;
        }
    }
}
