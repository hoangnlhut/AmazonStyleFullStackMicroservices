using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Commands
{
    public class UpdateProductHanlder : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHanlder(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var getProduct = await _productRepository.GetProductByIdAsync(request.Id);
            if (getProduct == null) throw new KeyNotFoundException("product not found");

            var getBrand = await _productRepository.GetBrandByIdAsync(request.BrandId);
            var getType = await _productRepository.GetTypeByIdAsync(request.TypeId);

            if (getBrand == null || getType == null) throw new KeyNotFoundException("invalid brand and type specified");

            var updatedProduct = request.ToUpdateEntity(getProduct, getBrand, getType);

            var result = await _productRepository.UpdateProductAsync(updatedProduct);
            return result;
        }
    }
}
