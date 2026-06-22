using Dapper;
using Discount.Entities;
using Discount.Models;
using Npgsql;

namespace Discount.Repositories
{
    public class DiscountPostgreRepository : IDiscountRepository
    {
        private readonly DiscountDatabaseSettings _dbConnection;

        public DiscountPostgreRepository(DiscountDatabaseSettings dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<bool> CreateCoupon(Coupon coupon)
        {
           await using var connection = new NpgsqlConnection(_dbConnection.ConnectionString);
           var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", coupon);
            return affected > 0;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_dbConnection.ConnectionString);
            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id", coupon);
            return affected > 0;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            await using var connection = new NpgsqlConnection(_dbConnection.ConnectionString);
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            return affected > 0;
        }

        public async Task<Coupon> GetCouponById(int id)
        {
            await using var connection = new NpgsqlConnection(_dbConnection.ConnectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE Id = @CouponId", new { CouponId = id });
            return coupon ?? NullCoupon.Instance;
        }   

        public async Task<Coupon> GetCouponByProductName(string productName)
        {
            await using var connection = new NpgsqlConnection(_dbConnection.ConnectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            return coupon ?? NullCoupon.Instance;
        }
    }
}
