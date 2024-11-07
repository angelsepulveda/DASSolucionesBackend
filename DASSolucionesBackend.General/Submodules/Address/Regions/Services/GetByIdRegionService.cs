using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Exceptions;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Services;

internal sealed class GetByIdRegionService(IGeneralDbContext dbContext) : IGetByIdRegionService
{
    public async Task<Region> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        Region? region =
            await dbContext.Regions.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (region is null) throw new RegionNotFoundException(id);
        
        return region;
    }
}