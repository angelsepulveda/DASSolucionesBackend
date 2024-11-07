using System.Reflection;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Data;

internal class GeneralDbContext : DbContext, IGeneralDbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options)
    {
    }
    
    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Region> Regions => Set<Region>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("General");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}