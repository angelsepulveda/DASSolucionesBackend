using DASSolucionesBackend.Warehouses.Submodules.Categories.Enttities;

namespace DASSolucionesBackend.Warehouses.Data.Contracts;

public interface IWareHousesDbContext
{
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}