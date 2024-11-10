using DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Features.Register;

public sealed record RegisterVoucherTypePayloadDto(string Name, string? Code, string? Description);

public record RegisterVoucherTypeCommand(RegisterVoucherTypePayloadDto PayloadDto)
    : ICommand<RegisterVoucherTypeResult>;

public sealed record RegisterVoucherTypeResult(Guid Id);

public class RegisterVoucherTypeCommandValidator : AbstractValidator<RegisterVoucherTypeCommand>
{
    public RegisterVoucherTypeCommandValidator()
    {
        RuleFor(x => x.PayloadDto.Name)
            .NotEmpty().WithName("El nombre es requerido")
            .NotNull().WithMessage("El nombre es requerido")
            .MaximumLength(30).WithMessage("El nombre no puede exceder los 30 caracteres");

        RuleFor(x => x.PayloadDto.Code)
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres");

        RuleFor(x => x.PayloadDto.Description)
            .MaximumLength(255).WithMessage("La descripción no puede exceder los 255 caracteres");
    }
}

internal class
    RegisterVoucherTypeCommandHandler(IGeneralDbContext dbContext)
    : ICommandHandler<RegisterVoucherTypeCommand, RegisterVoucherTypeResult>
{
    public async Task<RegisterVoucherTypeResult> Handle(RegisterVoucherTypeCommand request,
        CancellationToken cancellationToken)
    {
        VoucherType voucherType = VoucherType.Create(request.PayloadDto.Name, request.PayloadDto.Code,
            request.PayloadDto.Description);

        dbContext.VoucherTypes.Add(voucherType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterVoucherTypeResult(voucherType.Id);
    }
}