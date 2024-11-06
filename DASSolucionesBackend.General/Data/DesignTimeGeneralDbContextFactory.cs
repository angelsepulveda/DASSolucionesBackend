using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DASSolucionesBackend.General.Data;

internal class DesignTimeGeneralDbContextFactory : IDesignTimeDbContextFactory<GeneralDbContext>
{
    public GeneralDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        DbContextOptionsBuilder<GeneralDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new GeneralDbContext(optionsBuilder.Options);
    }
}