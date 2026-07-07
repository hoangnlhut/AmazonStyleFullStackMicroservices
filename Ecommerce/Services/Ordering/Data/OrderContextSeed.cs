using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seeded database with initial orders.");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "hoang",
                    FirstName = "Hoang",
                    LastName = "Nguyen",
                    EmailAddress = "hoang.nguyen@gmail.com",
                    AddressLine = "Bangalore",
                    Country = "Vietnam",
                    TotalPrice = 750,
                    State = "KA",
                    ZipCode = "10000",
                    CardName = "Visa",
                    CardNumber = "4111111111111111",
                    CreatedBy = "hoang",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "hoang",
                    LastModifiedDate = new DateTime(),
                }
            };
        }
    }
}
