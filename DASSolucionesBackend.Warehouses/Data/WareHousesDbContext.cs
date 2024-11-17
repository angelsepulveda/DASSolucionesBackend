using System.Reflection;
using DASSolucionesBackend.Warehouses.Data.Contracts;
using DASSolucionesBackend.Warehouses.Submodules.Categories.Enttities;
using Microsoft.EntityFrameworkCore;

namespace DASSolucionesBackend.Warehouses.Data;

internal class WareHousesDbContext : DbContext, IWareHousesDbContext
{
    public WareHousesDbContext(DbContextOptions<WareHousesDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Warehouses");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}