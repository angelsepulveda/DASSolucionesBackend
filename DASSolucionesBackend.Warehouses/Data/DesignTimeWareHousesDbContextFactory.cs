using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DASSolucionesBackend.Warehouses.Data;

internal class DesignTimeWareHousesDbContextFactory : IDesignTimeDbContextFactory<WareHousesDbContext>
{
    public WareHousesDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        DbContextOptionsBuilder<WareHousesDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new WareHousesDbContext(optionsBuilder.Options);
    }
}