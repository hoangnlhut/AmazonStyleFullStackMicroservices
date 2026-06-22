using Discount.Entities;

namespace Discount.Models
{
    public sealed class NullCoupon 
    {
        private static readonly Coupon _nullCoupon = new Coupon()
        {
            Id = 0,
            ProductName = "No Discount",
            Description = "No Discount Description",
            Amount = 0
        };

        private NullCoupon() { }

        public static Coupon Instance => _nullCoupon;
    }
}
