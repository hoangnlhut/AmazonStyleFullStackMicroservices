using Basket.Commands;
using Basket.Entities;
using Basket.Responses;
using MediatR;

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

        public static ShoppingCart CommandToEntity(this CreateBasketCommand command)
        {
            return new ShoppingCart(command.UserName)
            {
                Items = command.Items.Select(i => new ShoppingCartItem
                {
                    ImageFile = i.ImageFile,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
