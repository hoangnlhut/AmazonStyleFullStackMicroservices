using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Extentions
{
    public static class BrandMapper
    {
        public static IEnumerable<BrandResponse> ToResponseList(this IEnumerable<ProductBrand> brands)
        {
            return brands.Select(brand => brand.ToResponse()).ToList();
        }

        public static BrandResponse ToResponse(this ProductBrand brand)
        {
            return new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
    }
}