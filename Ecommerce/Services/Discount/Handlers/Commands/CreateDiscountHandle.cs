using Discount.Commands;
using Discount.Mappers;
using Discount.Repositories;
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
            var coupon = request.coupon.ToEntity();
            var result = await _discountRepository.CreateCoupon(coupon);
            return result;
        }
    }
}
