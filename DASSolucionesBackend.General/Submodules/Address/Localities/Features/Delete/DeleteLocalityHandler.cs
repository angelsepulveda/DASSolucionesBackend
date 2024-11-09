using DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Delete;

public sealed record DeleteLocalityCommand(Guid Id)
    : ICommand;

internal class DeleteLocalityCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdLocalityService getByIdLocalityService) : ICommandHandler<DeleteLocalityCommand>
{
    public async Task<Unit> Handle(DeleteLocalityCommand request, CancellationToken cancellationToken)
    {
        Locality locality =
            await getByIdLocalityService.HandleAsync(request.Id, cancellationToken);

        locality.ChangeStatus(false);

        dbContext.Localities.Update(locality);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}