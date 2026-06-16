namespace Basket.Responses
{
    public record BasketResponse
    {
        public string UserName { get; set; }
        public List<BasketItemResponse> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        public BasketResponse()
        {
            UserName = string.Empty;
            Items = new List<BasketItemResponse>();
        }

        public BasketResponse(string userName) : this(userName, new List<BasketItemResponse>())
        {
            
        }

      public BasketResponse(string userName, List<BasketItemResponse> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
