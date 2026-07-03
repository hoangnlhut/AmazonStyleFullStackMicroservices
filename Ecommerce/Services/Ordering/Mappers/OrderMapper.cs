using Ordering.Commands;
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

        public static Order ToEntity(this CheckoutOrderCommand cmd)
        {
            return new Order
            {
                UserName = cmd.UserName,
                TotalPrice = cmd.TotalPrice,
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                EmailAddress = cmd.EmailAddress,
                AddressLine = cmd.AddressLine,
                Country = cmd.Country,
                State = cmd.State,
                ZipCode = cmd.ZipCode,
                CardName = cmd.CardName,
                CardNumber = cmd.CardNumber,
                Expiration = cmd.Expiration,
                Cw = cmd.Cw,
                PaymentMethod = cmd.PaymentMethod
            };
        }
    }
}
