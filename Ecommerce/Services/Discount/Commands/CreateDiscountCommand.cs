using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record CreateDiscountCommand(CouponInput coupon) : IRequest<bool>;
}
