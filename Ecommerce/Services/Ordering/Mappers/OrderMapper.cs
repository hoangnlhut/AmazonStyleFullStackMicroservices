using Ordering.DTOs;
using Ordering.Entities;

namespace Ordering.Mappers
{
    public static class OrderMapper
    {
        public static IEnumerable<OrderDto> ToDtos(this IEnumerable<Order> entities)
        {
            return entities.Select(order => order.ToDto());
        }

        public static OrderDto ToDto(this Order entity)
        {
            return new OrderDto(entity.Id, 
                entity.UserName, 
                entity.TotalPrice ?? 0,
                entity.FirstName, 
                entity.LastName, 
                entity.EmailAddress,
                entity.AddressLine, 
                entity.Country, 
                entity.State, 
                entity.ZipCode, 
                entity.CardName, 
                entity.CardNumber, 
                entity.Expiration, 
                entity.Cw, 
                entity.PaymentMethod ?? 0);
        }
    }
}
