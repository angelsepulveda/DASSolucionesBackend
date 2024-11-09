using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Update;

public sealed record UpdateRegionPayloadDto(Guid Id, string Name, Guid CountryId, string? Code);

public sealed record UpdateRegionCommand(UpdateRegionPayloadDto PayloadDto)
    : ICommand;

internal class UpdateRegionCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdRegionService getByIdRegionService,
    IGetByIdCountryService getByIdCountryService) : ICommandHandler<UpdateRegionCommand>
{
    public async Task<Unit> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        await getByIdCountryService.HandleAsync(request.PayloadDto.CountryId, cancellationToken);
        
        Region region =
            await getByIdRegionService.HandleAsync(request.PayloadDto.Id, cancellationToken);

        region.Update(request.PayloadDto.Name, request.PayloadDto.CountryId, request.PayloadDto.Code);

        dbContext.Regions.Update(region);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}