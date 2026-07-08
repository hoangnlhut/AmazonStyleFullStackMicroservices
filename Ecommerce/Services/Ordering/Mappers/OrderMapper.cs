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
                entity.Cvv, 
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
                Cvv = cmd.Cvv,
                PaymentMethod = cmd.PaymentMethod
            };
        }

        public static Order MapUpdate(this Order origin, UpdateOrderCommand request)
        {
            origin.UserName = request.UserName;
            origin.TotalPrice = request.TotalPrice;
            origin.FirstName = request.FirstName;
            origin.LastName = request.LastName;
            origin.EmailAddress = request.EmailAddress;
            origin.AddressLine = request.AddressLine;
            origin.Country = request.Country;
            origin.State = request.State;
            origin.ZipCode = request.ZipCode;
            origin.CardName = request.CardName;
            origin.CardNumber = request.CardNumber;
            origin.Expiration = request.Expiration;
            origin.Cvv = request.Cvv;
            origin.PaymentMethod = request.PaymentMethod;

            return origin;
        }

        public static CheckoutOrderCommand ToCommand(this CreateOrderDto dto)
        {
            return new CheckoutOrderCommand
            {
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice ?? 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod ?? 0
            };
        }

        public static UpdateOrderCommand ToCommand(this OrderDto dto)
        {
            return new UpdateOrderCommand
            {
                Id = dto.Id,
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice ?? 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod ?? 0
            };
        }
    }
}
