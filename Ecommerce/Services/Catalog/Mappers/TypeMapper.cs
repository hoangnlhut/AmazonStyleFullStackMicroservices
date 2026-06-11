using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Mappers
{
    public static class TypeMapper
    {
        public static IEnumerable<TypeResponse> ToResponseList(this IEnumerable<ProductType> types)
        {
            return types.Select(type => type.ToResponse()).ToList();
        }

        public static TypeResponse ToResponse(this ProductType type)
        {
            return new TypeResponse
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        public static IEnumerable<ProductType> ToDtos(this IEnumerable<TypeResponse> typeResponses)
        {
            return typeResponses.Select(response => response.ToDto()).ToList();
        }

        public static ProductType ToDto(this TypeResponse response)
        {
            return new ProductType
            {
                Id = response.Id,
                Name = response.Name ?? string.Empty
            };
        }
    }
}