using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Cards.API.Extensions
{
    public static class AppExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            IWebHostEnvironment env = host.Services.GetRequiredService<IWebHostEnvironment>();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {

                    var retryPolicy = Policy.Handle<SqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError(message: $"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

                    // Invoke Migrate and Seed Intial Data
                    MigrateAndSeedData(seeder!, context, services);

                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName} {fn}", typeof(TContext).Name, "MigrateDatabase");
                }
            }

            return host;
        }
        private static void MigrateAndSeedData<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
           where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
