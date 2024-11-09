using DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;
using DASSolucionesBackend.General.Submodules.Address.Localities.Exceptions;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Services;

internal sealed class GetByIdLocalityService(IGeneralDbContext dbContext) : IGetByIdLocalityService
{
    public async Task<Locality> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        Locality? locality =
            await dbContext.Localities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (locality is null) throw new LocalityNotFoundException(id);

        return locality;
    }
}