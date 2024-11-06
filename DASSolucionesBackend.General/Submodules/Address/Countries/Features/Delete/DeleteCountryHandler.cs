using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Delete;

public sealed record DeleteCountryCommand(Guid Id)
    : ICommand;

internal class DeleteCountryCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdCountryService getByIdCountryService) : ICommandHandler<DeleteCountryCommand>
{
    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        Country country =
            await getByIdCountryService.HandleAsync(request.Id, cancellationToken);

        country.ChangeStatus(false);

        dbContext.Countries.Update(country);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}