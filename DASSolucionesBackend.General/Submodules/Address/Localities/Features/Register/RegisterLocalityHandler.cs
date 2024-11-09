using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Contracts.Services;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Features.Register;

public sealed record RegisterLocalityPayloadDto(string Name, Guid CountryId, Guid RegionId, string? Code);

public record RegisterLocalityCommand(RegisterLocalityPayloadDto PayloadDto)
    : ICommand<RegisterLocalityResult>;

public sealed record RegisterLocalityResult(Guid Id);

public class RegisterLocalityCommandValidator : AbstractValidator<RegisterLocalityCommand>
{
    public RegisterLocalityCommandValidator()
    {
        RuleFor(x => x.PayloadDto.Name)
            .NotEmpty().WithName("El nombre es requerido")
            .NotNull().WithMessage("El nombre es requerido")
            .MaximumLength(200).WithMessage("El nombre no puede exceder los 30 caracteres");

        RuleFor(x => x.PayloadDto.Code)
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres");

        RuleFor(x => x.PayloadDto.CountryId)
            .NotNull().WithMessage("El país es requerido");

        RuleFor(x => x.PayloadDto.RegionId)
            .NotNull().WithMessage("La region es requerido");
    }
}

internal class
    RegisterLocalityCommandHandler(
        IGeneralDbContext dbContext,
        IGetByIdCountryService getByIdCountryService,
        IGetByIdRegionService getByIdRegionService)
    : ICommandHandler<RegisterLocalityCommand, RegisterLocalityResult>
{
    public async Task<RegisterLocalityResult> Handle(RegisterLocalityCommand request,
        CancellationToken cancellationToken)
    {
        await getByIdRegionService.HandleAsync(request.PayloadDto.RegionId, cancellationToken);
        await getByIdCountryService.HandleAsync(request.PayloadDto.CountryId, cancellationToken);

        Locality locality = Locality.Create(request.PayloadDto.Name, request.PayloadDto.RegionId,
            request.PayloadDto.CountryId, request.PayloadDto.Code);

        dbContext.Localities.Add(locality);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterLocalityResult(locality.Id);
    }
}