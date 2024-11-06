using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Update;

public sealed record UpdateCountryPayloadDto(Guid Id,string Name, string? Code, string? Demonym);

public sealed record UpdateCountryCommand(UpdateCountryPayloadDto PayloadDto)
    : ICommand;

internal class UpdateCountryCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdCountryService getByIdCountryService) : ICommandHandler<UpdateCountryCommand>
{
    public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        Country country =
            await getByIdCountryService.HandleAsync(request.PayloadDto.Id, cancellationToken);
        
        country.Update(request.PayloadDto.Name,request.PayloadDto.Code,request.PayloadDto.Demonym);

        dbContext.Countries.Update(country);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}