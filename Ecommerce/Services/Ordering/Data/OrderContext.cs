using Microsoft.EntityFrameworkCore;
using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
    }
}
