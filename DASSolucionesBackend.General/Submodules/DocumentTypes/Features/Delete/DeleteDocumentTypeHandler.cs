using DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Delete;

public record DeleteDocumentTypeCommand(Guid Id)
    : ICommand;

internal class DeleteDocumentTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdDocumentTypeService getByIdDocumentTypeService) : ICommandHandler<DeleteDocumentTypeCommand>
{
    public async Task<Unit> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        DocumentType documentType =
            await getByIdDocumentTypeService.HandleAsync(request.Id, cancellationToken);

        documentType.ChangeStatus(false);

        dbContext.DocumentTypes.Update(documentType);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}