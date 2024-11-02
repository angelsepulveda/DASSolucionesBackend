using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Register;

public record RegisterDocumentTypeCommand(RegisterDocumentTypePayloadDto PayloadDto)
    : ICommand<RegisterDocumentTypeResult>;

public sealed record RegisterDocumentTypeResult(Guid Id);

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