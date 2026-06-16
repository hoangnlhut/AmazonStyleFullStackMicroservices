namespace Basket.Responses
{
    public record ShoppingCartResponse
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemResponse> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        public ShoppingCartResponse()
        {
            UserName = string.Empty;
            Items = new List<ShoppingCartItemResponse>();
        }

        public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingCartItemResponse>())
        {
            
        }

      public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
