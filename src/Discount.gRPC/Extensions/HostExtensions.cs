using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.api.gRPC.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                //migrate database
                try
                {

                    logger.LogInformation("migrating postgresql database");
                    using var connection = new
                        NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                            ProductName VARCHAR(200) NOT NULL,
                                            Description TEXT,
                                            Amount INT)";
                    command.ExecuteNonQuery();
                    //seed data 
                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) 
                        VALUES ('IPhone Discount','Very Good Phone',200)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) 
                        VALUES ('Test Discount','Very Good Test',700)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) 
                        VALUES ('Samsung Discount','Very Good Phone',100)";
                    command.ExecuteNonQuery();
                    
                    logger.LogInformation("migration has been completed");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(@$"an error has occured!
                        error content : {ex}");
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host,retryForAvailability);
                    }
                }
            }
            return host;
        }
    }
}
