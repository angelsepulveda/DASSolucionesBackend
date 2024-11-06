using System.Reflection;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DASSolucionesBackend.General.Data;

internal class GeneralDbContext : DbContext, IGeneralDbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options)
    {
    }
    
    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<Country> Countries => Set<Country>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("General");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}