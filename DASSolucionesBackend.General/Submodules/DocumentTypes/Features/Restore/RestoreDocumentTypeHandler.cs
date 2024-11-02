using DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Restore;

public record RestoreDocumentTypeCommand(Guid Id)
    : ICommand;

internal class RestoreDocumentTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdDocumentTypeService getByIdDocumentTypeService) : ICommandHandler<RestoreDocumentTypeCommand>
{
    public async Task<Unit> Handle(RestoreDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        DocumentType documentType =
            await getByIdDocumentTypeService.HandleAsync(request.Id, cancellationToken);
        
        documentType.ChangeStatus(true);

        dbContext.DocumentTypes.Update(documentType);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}