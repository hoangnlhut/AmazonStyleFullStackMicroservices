using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Entities;
using System.Linq.Expressions;

namespace Ordering.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
           var orderList = await _orderContext.Orders
                .AsNoTracking() // for better performance
                .Where(o => o.UserName == userName)
                .ToListAsync();
           return orderList;
        }
    }
}
