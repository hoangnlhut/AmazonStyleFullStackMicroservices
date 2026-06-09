using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductsByNameQuery(string name) : IRequest<IEnumerable<ProductResponse>>
    {
    }
}
