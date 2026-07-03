using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Entities;
using Ordering.Handlers.Commands;

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

        public static Order CheckoutToEntity(this CheckoutOrderCommand cmd)
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

        public static Order MapUpdate(this Order origin, UpdateOrderCommand cmd)
        {
            origin.UserName = cmd.UserName;
            origin.TotalPrice = cmd.TotalPrice;
            origin.FirstName = cmd.FirstName;
            origin.LastName = cmd.LastName;
            origin.EmailAddress = cmd.EmailAddress;
            origin.AddressLine = cmd.AddressLine;
            origin.Country = cmd.Country;
            origin.State = cmd.State;
            origin.ZipCode = cmd.ZipCode;
            origin.CardName = cmd.CardName;
            origin.CardNumber = cmd.CardNumber;
            origin.Expiration = cmd.Expiration;
            origin.Cw = cmd.Cw;
            origin.PaymentMethod = cmd.PaymentMethod;

            return origin;
        }
    }
}
