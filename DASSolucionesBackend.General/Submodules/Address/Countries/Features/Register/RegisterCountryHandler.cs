using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Countries.Features.Register;

public sealed record RegisterCountryPayloadDto(string Name, string Code, string Demonym);

public record RegisterCountryCommand(RegisterCountryPayloadDto PayloadDto)
    : ICommand<RegisterCountryResult>;

public sealed record RegisterCountryResult(Guid Id);

public class RegisterCountryCommandValidator : AbstractValidator<RegisterCountryCommand>
{
    public RegisterCountryCommandValidator()
    {
        RuleFor(x => x.PayloadDto.Name)
            .NotEmpty().WithName("El nombre es requerido")
            .NotNull().WithMessage("El nombre es requerido")
            .MaximumLength(200).WithMessage("El nombre no puede exceder los 200 caracteres");

        RuleFor(x => x.PayloadDto.Code)
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres");

        RuleFor(x => x.PayloadDto.Demonym)
            .MaximumLength(255).WithMessage("El gentilicio no puede exceder los 255 caracteres");
    }
}

internal class
    RegisterCountryCommandHandler(IGeneralDbContext dbContext)
    : ICommandHandler<RegisterCountryCommand, RegisterCountryResult>
{
    public async Task<RegisterCountryResult> Handle(RegisterCountryCommand request, CancellationToken cancellationToken)
    {
        Country country = Country.Create(request.PayloadDto.Name, request.PayloadDto.Code,
            request.PayloadDto.Demonym);

        dbContext.Countries.Add(country);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterCountryResult(country.Id);
    }
}