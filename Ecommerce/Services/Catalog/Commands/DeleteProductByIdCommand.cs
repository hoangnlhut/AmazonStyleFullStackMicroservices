using MediatR;

namespace Catalog.Commands
{
    public record DeleteProductByIdCommand : IRequest<bool>
    {
        public string Id { get; init; }
    }
}
