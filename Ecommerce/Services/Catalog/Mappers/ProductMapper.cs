using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;

namespace Catalog.Mappers
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

        public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        {
            return products.Select(product => product.ToResponse()).ToList();
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

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                Summary = command.Summary,
                ImageFile = command.ImageFile,
                CreatedDate = DateTimeOffset.UtcNow,
                Brand = brand,
                Type = type
            };
        }

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing ,ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Id = existing.Id,
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                Summary = command.Summary,
                ImageFile = command.ImageFile,
                CreatedDate = existing.CreatedDate,
                Brand = brand,
                Type = type
            };
        }

        public static Pagination<ProductDto> ToPaginationDtos(this Pagination<ProductResponse> products)
        {
            return new Pagination<ProductDto>
            {
                Data = products.Data.Select(product => product.ToDto()).ToList(),
                Count = products.Count,
                PageSize = products.PageSize,
                PageIndex = products.PageIndex
            };
        }

        public static IEnumerable<ProductDto> ToDtos(this IEnumerable<ProductResponse> products)
        {
            return products.Select(product => product.ToDto()).ToList();
        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if(product == null) return null;    

            return new ProductDto(
                product.Id, 
                product.Name, 
                product.Description, 
                product.Summary, 
                product.ImageFile, 
                new ProductBrandDto { Id = product.Brand.Id, Name = product.Brand.Name }, 
                new ProductTypeDto { Id = product.Type.Id, Name = product.Type.Name }, 
                product.Price, 
                product.CreatedDate);
            
        }
    }
}