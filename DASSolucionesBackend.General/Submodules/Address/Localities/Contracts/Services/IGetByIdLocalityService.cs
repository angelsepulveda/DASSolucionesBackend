using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;

public interface IGetByIdLocalityService
{
    Task<Locality> HandleAsync(Guid id, CancellationToken cancellationToken);
}