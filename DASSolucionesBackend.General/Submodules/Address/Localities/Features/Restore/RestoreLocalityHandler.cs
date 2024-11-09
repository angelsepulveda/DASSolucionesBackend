using DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Restore;

public sealed record RestoreLocalityCommand(Guid Id)
    : ICommand;

internal class RestoreLocalityCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdLocalityService getByIdLocalityService) : ICommandHandler<RestoreLocalityCommand>
{
    public async Task<Unit> Handle(RestoreLocalityCommand request, CancellationToken cancellationToken)
    {
        Locality locality =
            await getByIdLocalityService.HandleAsync(request.Id, cancellationToken);

        locality.ChangeStatus(true);

        dbContext.Localities.Update(locality);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}