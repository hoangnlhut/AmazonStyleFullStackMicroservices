namespace Discount.Extensions
{
    public static class DbExtention
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<DiscountDatabaseSettings>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    var connectionString = configuration.ConnectionString;
                    logger.LogInformation("Discount Migrating database with connection string: {ConnectionString}", connectionString);
                    ApplyMigration(connectionString);
                    logger.LogInformation("Database migration completed successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw; // Optionally rethrow or handle the exception as needed.
                }
            }
            return host;
        }

        private static void ApplyMigration(string connectionString)
        {
            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    using var connection = new Npgsql.NpgsqlConnection(connectionString);
                    connection.Open();
                    using var command = new Npgsql.NpgsqlCommand
                    {
                        Connection = connection,
                    };
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Coupon (
                            Id SERIAL PRIMARY KEY,
                            ProductName VARCHAR(24) NOT NULL,
                            Description TEXT,
                            Amount INT
                        );";

                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Adidas Quick Force Indoor Badminton Shoes', 'Shoes Discount', 500)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700)";
                    command.ExecuteNonQuery();

                    break; // Exit the loop if migration is successful
                }
                catch (Exception ex)
                {
                    retry--;
                    if (retry == 0)
                    {
                        throw; // Rethrow the exception if all retries fail
                    }
                    Thread.Sleep(2000); // Wait for 2 seconds before retrying
                }
            }
        }
    }
}
