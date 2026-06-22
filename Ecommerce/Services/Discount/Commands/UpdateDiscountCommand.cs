using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record UpdateDiscountCommand(CouponInput couponInput) : IRequest<bool>;
}
