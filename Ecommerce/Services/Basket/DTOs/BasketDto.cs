using Basket.Responses;

namespace Basket.DTOs
{
    public record BasketDto(string UserName, List<BasketItemDto> Items, decimal TotalPrice);
    
}
