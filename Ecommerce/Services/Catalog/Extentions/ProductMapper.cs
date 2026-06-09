using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;

namespace Catalog.Extentions
{
    public static class ProductMapper
    {
        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> products)
        {
            return new Pagination<ProductResponse>
            {
                Data = products.Data.Select(product => product.ToResponse()).ToList(),
                Count = products.Count,
                PageSize = products.PageSize,
                PageIndex = products.PageIndex
            };
        }

        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Summary = product.Summary,
                ImageFile = product.ImageFile,
                CreatedDate = product.CreatedDate,
                Brand = new BrandResponse { Id = product.Brand.Id, Name = product.Brand.Name },
                Type = new TypeResponse { Id = product.Type.Id, Name = product.Type.Name }
            };
        }
    }
}