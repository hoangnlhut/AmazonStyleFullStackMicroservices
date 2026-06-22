using Discount.DTOs;
using Discount.Entities;

namespace Discount.Mappers
{
    public static class DiscountMapper
    {
        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
        }

        //public static Coupon ToEntity(this CouponDto couponDto)
        //{
        //    return new Coupon(couponDto.Id, couponDto.ProductName, couponDto.Description, couponDto.Amount);
        //}
    }
}
