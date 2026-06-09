using Catalog.Extentions;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Mappers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetBrandsAsync();
            return brandList.ToResponseList();
        }
    }
}