using MediatR;

namespace Ordering.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<Unit>
    {
    }
}
