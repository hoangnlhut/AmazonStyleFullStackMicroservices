using Discount.Entities;

namespace Discount.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCouponByProductName(string productName);
        Task<Coupon> GetCouponById(int id);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productName);
    }
}
