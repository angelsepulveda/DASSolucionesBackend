using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Data.Contracts;

public interface IGeneralDbContext
{
    DbSet<DocumentType> DocumentTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}