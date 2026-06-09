using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers.Queries
{
    public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByBrandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductsByBrandAsync(request.brand);
            return product.ToResponseList();
        }
    }
}