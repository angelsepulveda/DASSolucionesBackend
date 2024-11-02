using DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Features.Update;

public record UpdateDocumentTypeCommand(UpdateDocumentTypePayloadDto PayloadDto)
    : ICommand;

internal class UpdateDocumentTypeCommandHandler(
    IGeneralDbContext dbContext,
    IGetByIdDocumentTypeService getByIdDocumentTypeService) : ICommandHandler<UpdateDocumentTypeCommand>
{
    public async Task<Unit> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        DocumentType documentType =
            await getByIdDocumentTypeService.HandleAsync(request.PayloadDto.Id, cancellationToken);
        
        documentType.Update(request.PayloadDto.Name,request.PayloadDto.Code,request.PayloadDto.Description);

        dbContext.DocumentTypes.Update(documentType);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}