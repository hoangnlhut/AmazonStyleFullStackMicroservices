using Discount.DTOs;
using Discount.Entities;
using MediatR;

namespace Discount.Queries
{
    public record GetDiscountByProductNameQuery(string productName) : IRequest<CouponDto>
    {
    }
}
