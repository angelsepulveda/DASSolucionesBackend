namespace DASSolucionesBackend.Warehouses.Data.Contracts;

public interface IWareHousesDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}