using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductsByBrandQuery(string brand) : IRequest<IEnumerable<ProductResponse>>
    {
    }
}
