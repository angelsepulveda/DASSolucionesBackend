using DASSolucionesBackend.General.Submodules.Address.Countries.Contracts.Services;
using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Countries.Exceptions;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Features.Register;

public sealed record RegisterRegionPayloadDto(string Name, Guid CountryId, string? Code);

public record RegisterRegionCommand(RegisterRegionPayloadDto PayloadDto)
    : ICommand<RegisterRegionResult>;

public sealed record RegisterRegionResult(Guid Id);

public class RegisterRegionCommandValidator : AbstractValidator<RegisterRegionCommand>
{
    public RegisterRegionCommandValidator()
    {
        RuleFor(x => x.PayloadDto.Name)
            .NotEmpty().WithName("El nombre es requerido")
            .NotNull().WithMessage("El nombre es requerido")
            .MaximumLength(200).WithMessage("El nombre no puede exceder los 30 caracteres");

        RuleFor(x => x.PayloadDto.Code)
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres");

        RuleFor(x => x.PayloadDto.CountryId)
            .NotNull().WithMessage("El país es requerido");
    }
}

internal class
    RegisterRegionCommandHandler(IGeneralDbContext dbContext, IGetByIdCountryService getByIdCountryService)
    : ICommandHandler<RegisterRegionCommand, RegisterRegionResult>
{
    public async Task<RegisterRegionResult> Handle(RegisterRegionCommand request, CancellationToken cancellationToken)
    {
        await getByIdCountryService.HandleAsync(request.PayloadDto.CountryId, cancellationToken);

        Region region = Region.Create(request.PayloadDto.Name, request.PayloadDto.CountryId, request.PayloadDto.Code);

        dbContext.Regions.Add(region);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterRegionResult(region.Id);
    }
}