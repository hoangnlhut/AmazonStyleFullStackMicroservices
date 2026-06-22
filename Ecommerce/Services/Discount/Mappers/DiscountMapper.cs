using Discount.DTOs;
using Discount.Entities;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Discount.Mappers
{
    public static class DiscountMapper
    {
        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
        }

        public static Coupon ToEntity(this CouponInput couponInput)
        {
            return new Coupon
            {
                Id = couponInput.Id,
                ProductName = couponInput.ProductName,
                Description = couponInput.Description,
                Amount = couponInput.Amount
            };
        }
    }
}
