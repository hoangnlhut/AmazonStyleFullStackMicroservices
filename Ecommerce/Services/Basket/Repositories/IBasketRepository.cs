using Basket.Entities;

namespace Basket.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpsertBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
