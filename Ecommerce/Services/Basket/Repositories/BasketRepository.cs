using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var cachedData = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(cachedData))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(cachedData);
        }

        public async Task<ShoppingCart> UpsertBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }
    }
}
