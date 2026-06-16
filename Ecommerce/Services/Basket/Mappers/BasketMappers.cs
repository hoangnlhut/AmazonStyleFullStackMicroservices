using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers
{
    public static class BasketMappers
    {
        public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartResponse(shoppingCart.UserName, shoppingCart.Items.Select(item => item.ItemToResponse()).ToList());
        }
        public static ShoppingCartItemResponse ItemToResponse(this ShoppingCartItem shoppingCartItem)
        {
            return new ShoppingCartItemResponse
            {
                ProductId = shoppingCartItem.ProductId,
                ProductName = shoppingCartItem.ProductName,
                ImageFile = shoppingCartItem.ImageFile,
                Price = shoppingCartItem.Price,
                Quantity = shoppingCartItem.Quantity
            };
        }
    }
}
