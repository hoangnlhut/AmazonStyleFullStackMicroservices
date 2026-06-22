using Discount.Commands;
using Discount.Mappers;
using Discount.Repositories;
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
            var coupon = request.couponInput.ToEntity();
            var result = await _discountRepository.UpdateCoupon(coupon);
            return result;
        }
    }
}
