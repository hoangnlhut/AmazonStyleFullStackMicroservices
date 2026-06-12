using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Mappers
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

        public static IEnumerable<BrandDto> ToDtos(this IEnumerable<BrandResponse> brands)
        {
            return brands.Select(brand => brand.ToDto()).ToList();
        }

        public static BrandDto ToDto(this BrandResponse brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
    }
}