using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MIRS.Persistence.ApplicationDbContext;
using MIRS.Domain.Interfaces;

namespace MIRS.Startup;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(
        IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();

        var provider = scope.ServiceProvider;
        var logger = provider.GetRequiredService<ILoggerFactory>()
                             .CreateLogger("DatabaseInitializer");

        try
        {
            var context = provider.GetRequiredService<ApplicationContext>();
            await context.Database.MigrateAsync(cancellationToken);

            var seeders = provider.GetServices<IDataSeeder>();
            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            throw; 
        }
    }
}
