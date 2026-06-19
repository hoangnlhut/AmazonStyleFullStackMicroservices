using Discount.Entities;

namespace Discount.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(int id, Coupon coupon);
        Task<bool> DeleteCoupon(string productName);
    }
}
