using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Register;

public record RegisterDocumentTypeCommand(RegisterDocumentTypePayloadDto PayloadDto)
    : ICommand<RegisterDocumentTypeResult>;

public sealed record RegisterDocumentTypeResult(Guid Id);

public class RegisterDocumentTypeCommandValidator : AbstractValidator<RegisterDocumentTypeCommand>
{
    public RegisterDocumentTypeCommandValidator()
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
    RegisterDocumentTypeCommandHandler(IGeneralDbContext dbContext)
    : ICommandHandler<RegisterDocumentTypeCommand, RegisterDocumentTypeResult>
{
    public async Task<RegisterDocumentTypeResult> Handle(RegisterDocumentTypeCommand request,
        CancellationToken cancellationToken)
    {
        DocumentType documentType = DocumentType.Create(request.PayloadDto.Name, request.PayloadDto.Code,
            request.PayloadDto.Description);

        dbContext.DocumentTypes.Add(documentType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterDocumentTypeResult(documentType.Id);
    }
}