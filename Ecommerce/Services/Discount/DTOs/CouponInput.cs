namespace Discount.DTOs
{
    public record CouponInput(string ProductName, string Description, int Amount, int Id = 0);
}
