namespace Basket.DTOs
{
    public record BasketItemDto(string ProductId, string ProductName, string ImageFile, decimal Price, string Quantity);
}
