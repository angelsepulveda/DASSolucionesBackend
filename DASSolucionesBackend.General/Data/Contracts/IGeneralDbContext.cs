using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Data.Contracts;

public interface IGeneralDbContext
{
    DbSet<DocumentType> DocumentTypes { get; }
    DbSet<Country> Countries { get; }
    DbSet<Region> Regions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}