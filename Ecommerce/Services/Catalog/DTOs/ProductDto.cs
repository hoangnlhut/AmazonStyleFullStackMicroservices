namespace Catalog.DTOs
{
    public record ProductDto(
        string Id,
        string Name,
        string Description,
        string Summary,
        string ImageFile,
        ProductBrandDto Brand,
        ProductTypeDto Type,
        decimal Price,
        DateTimeOffset CreatedDate);
}
