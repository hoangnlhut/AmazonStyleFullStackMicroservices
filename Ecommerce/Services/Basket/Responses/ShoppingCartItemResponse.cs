namespace Basket.Responses
{
    public record ShoppingCartItemResponse(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quantity
    );
}
