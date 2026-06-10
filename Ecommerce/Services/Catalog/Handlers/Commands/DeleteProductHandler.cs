using Catalog.Commands;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Commands
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var getProduct = await _productRepository.GetProductByIdAsync(request.Id);
            if (getProduct == null) throw new KeyNotFoundException("product not found");

            var result = await _productRepository.DeleteProductAsync(request.Id);
            return result;
        }
    }
}
