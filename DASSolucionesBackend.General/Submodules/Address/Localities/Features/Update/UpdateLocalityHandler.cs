using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Update;

public sealed record UpdateLocalityPayloadDto(Guid Id, string Name, Guid CountryId, Guid RegionId, string? Code);

public sealed record UpdateLocalityCommand(UpdateLocalityPayloadDto PayloadDto)
    : ICommand;

internal class UpdateLocalityLocalityHandler(
    IGeneralDbContext dbContext,
    IGetByIdLocalityService getByIdLocalityService,
    IGetByIdCountryService getByIdCountryService,
    IGetByIdRegionService getByIdRegionService) : ICommandHandler<UpdateLocalityCommand>
{
    public async Task<Unit> Handle(UpdateLocalityCommand request, CancellationToken cancellationToken)
    {
        await getByIdCountryService.HandleAsync(request.PayloadDto.CountryId, cancellationToken);
        await getByIdRegionService.HandleAsync(request.PayloadDto.RegionId, cancellationToken);

        Locality locality =
            await getByIdLocalityService.HandleAsync(request.PayloadDto.Id, cancellationToken);

        locality.Update(request.PayloadDto.Name, request.PayloadDto.RegionId, request.PayloadDto.CountryId,
            request.PayloadDto.Code);

        dbContext.Localities.Update(locality);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}