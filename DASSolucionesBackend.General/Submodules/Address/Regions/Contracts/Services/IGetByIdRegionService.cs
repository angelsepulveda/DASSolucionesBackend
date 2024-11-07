using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;

public interface IGetByIdRegionService
{
    Task<Region> HandleAsync(Guid id, CancellationToken cancellationToken);
}