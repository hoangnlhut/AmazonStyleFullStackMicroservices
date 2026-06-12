namespace Catalog.DTOs
{
    public record ProductDto(
        string Id,
        string Name,
        string Description,
        string Summary,
        string ImageFile,
        BrandDto Brand,
        TypeDto Type,
        decimal Price,
        DateTimeOffset CreatedDate);
}
