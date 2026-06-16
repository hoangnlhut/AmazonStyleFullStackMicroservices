namespace Basket.Responses
{
    public record ShoppingCartItemResponse
    {
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public string ImageFile { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
