using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var getBrand = await _productRepository.GetBrandByIdAsync(request.BrandId);
            var getType = await _productRepository.GetTypeByIdAsync(request.TypeId);

            if(getBrand == null || getType == null) throw new KeyNotFoundException("invalid brand and type specified");

            var productEntity = request.ToEntity(getBrand, getType);

            var newProduct = await _productRepository.CreateProductAsync(productEntity);
            return newProduct.ToResponse();
        }
    }
}
