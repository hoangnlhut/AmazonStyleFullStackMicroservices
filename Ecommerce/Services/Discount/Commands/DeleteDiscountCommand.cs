using MediatR;

namespace Discount.Commands
{
    public record DeleteDiscountCommand(string productName) : IRequest<bool>;
}
