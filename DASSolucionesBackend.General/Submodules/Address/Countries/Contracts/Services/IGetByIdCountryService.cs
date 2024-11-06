using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;

public interface IGetByIdCountryService
{
    Task<Country> HandleAsync(Guid id, CancellationToken cancellationToken);
}