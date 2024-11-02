using DASSolucionesBackend.General.Submodules.DocumentTypes.Contracts.Services;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;
using DASSolucionesBackend.General.Submodules.DocumentTypes.Exceptions;

namespace DASSolucionesBackend.General.Submodules.DocumentTypes.Services;

internal sealed class GetByIdDocumentTypeService(IGeneralDbContext dbContext) : IGetByIdDocumentTypeService
{
    public async Task<DocumentType> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        DocumentType? documentType =
            await dbContext.DocumentTypes.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (documentType is null) throw new DocumentTypeNotFoundException(id);

        return documentType;
    }
}