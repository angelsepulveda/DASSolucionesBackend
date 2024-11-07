using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Restore;

public sealed record RestoreRegionCommand(Guid Id)
    : ICommand;

internal class RestoreRegionCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdRegionService getByIdRegionService) : ICommandHandler<RestoreRegionCommand>
{
    public async Task<Unit> Handle(RestoreRegionCommand request, CancellationToken cancellationToken)
    {
        Region region =
            await getByIdRegionService.HandleAsync(request.Id, cancellationToken);

        region.ChangeStatus(true);

        dbContext.Regions.Update(region);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}