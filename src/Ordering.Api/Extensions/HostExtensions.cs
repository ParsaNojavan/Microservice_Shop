using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDataBase<TContext>(this IHost host
            , Action<TContext, IServiceProvider> seeder,
             int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("migrating...");
                    InvokeSeeder(seeder, context,services);
                    logger.LogInformation("migrating has been done");
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex,"An Error Occured while migrating Database");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability += 1;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDataBase<TContext>(host, seeder, retryForAvailability);
                    }
                    throw;
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext,IServiceProvider> seeder
            ,TContext context,IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
