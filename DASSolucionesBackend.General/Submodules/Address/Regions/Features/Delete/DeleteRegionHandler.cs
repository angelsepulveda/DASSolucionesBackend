using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Delete;

public sealed record DeleteRegionCommand(Guid Id)
    : ICommand;

internal class DeleteRegionCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdRegionService getByIdRegionService) : ICommandHandler<DeleteRegionCommand>
{
    public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        Region region =
            await getByIdRegionService.HandleAsync(request.Id, cancellationToken);

        region.ChangeStatus(false);

        dbContext.Regions.Update(region);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}