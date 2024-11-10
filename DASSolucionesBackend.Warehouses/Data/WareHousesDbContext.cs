using System.Reflection;
using DASSolucionesBackend.Warehouses.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DASSolucionesBackend.Warehouses.Data;

internal class WareHousesDbContext : DbContext, IWareHousesDbContext
{
    public WareHousesDbContext(DbContextOptions<WareHousesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Warehouses");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}