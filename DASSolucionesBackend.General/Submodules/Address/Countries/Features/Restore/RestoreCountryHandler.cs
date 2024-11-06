using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Restore;

public sealed record RestoreCountryCommand(Guid Id)
    : ICommand;

internal class RestoreCountryCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdCountryService getByIdCountryService) : ICommandHandler<RestoreCountryCommand>
{
    public async Task<Unit> Handle(RestoreCountryCommand request, CancellationToken cancellationToken)
    {
        Country country =
            await getByIdCountryService.HandleAsync(request.Id, cancellationToken);

        country.ChangeStatus(true);

        dbContext.Countries.Update(country);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}